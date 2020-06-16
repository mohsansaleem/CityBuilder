using System;
using PG.City.Context.Gameplay;
using PG.City.Model.data;
using UniRx;
using UnityEngine;
using Zenject;

namespace PG.City.Model.remote
{
    public class ModuleRemoteDataModel : EntityRemoteDataModel
    {
        [Inject] private readonly StaticDataModel _staticDataModel;
        [Inject] private readonly RemoteDataModel _remoteDataModel;
        [Inject] private readonly SignalBus _signalBus;

        // For the Timer
        private IDisposable _disposable;

        public readonly ReactiveProperty<ModuleRemoteData> ModuleRemoteData;
        public readonly ReactiveProperty<bool> IsInConstruction;
        public readonly ReactiveProperty<bool> IsInProduction;
        public readonly ReactiveProperty<bool> IsFull;
        public readonly ReactiveProperty<float> ProgressValue;

        public ModuleRemoteDataModel()
        {
            ModuleRemoteData = new ReactiveProperty<ModuleRemoteData>();
            IsInConstruction = new ReactiveProperty<bool>(false);
            IsInProduction = new ReactiveProperty<bool>(false);
            IsFull = new ReactiveProperty<bool>(false);
            ProgressValue = new ReactiveProperty<float>(0);
        }

        public ModuleData Data => ModuleRemoteData.Value.ModuleData;
        public ModuleRemoteData RemoteData => ModuleRemoteData.Value;

        public void SeedModuleRemoteData(ModuleRemoteData moduleRemoteData)
        {
            ModuleRemoteData.Value = moduleRemoteData;
            CurrentPosition.Value = moduleRemoteData.CurrentPosition;

            IsInConstruction.Value = moduleRemoteData.IsInConstruction;
            IsInProduction.Value = moduleRemoteData.IsInProduction;
            IsFull.Value = moduleRemoteData.IsFull;

            if (RemoteData.IsInConstruction)
            {
                Construct();
            }
            else if (!RemoteData.IsFull && RemoteData.IsInProduction)
            {
                Produce();
            }
        }

        public void Produce()
        {
            if (RemoteData.IsFull || RemoteData.IsInConstruction)
            {
                Debug.LogError("Alreading doing somethings");
                return;
            }

            Debug.LogError("Produce: "+DateTime.Now);
            
            if (!RemoteData.IsInProduction)
            {
                RemoteData.LastCommandTime = DateTime.Now;
            }

            _disposable?.Dispose();
            _disposable = null;

            // TODO: Run the Timer and Set the IsFull Reactive.
            _disposable = Observable.Timer(TimeSpan.FromSeconds(1)).Repeat().Subscribe((sec) =>
            {
                double diff = (DateTime.Now - RemoteData.LastCommandTime).TotalSeconds;

                ProgressValue.Value = (float) diff / Data.ProductionTimeUnit;

                if ((float) diff >= Data.ProductionTimeUnit)
                {
                    Debug.LogError("Produced: " + DateTime.Now);
                    
                    _disposable?.Dispose();

                    // TODO: Do this through Service.
                    RemoteData.IsInProduction = false;
                    RemoteData.IsFull = true;

                    // TODO: Do this in Command.
                    IsInProduction.Value = false;
                    IsFull.Value = true;
                }
            });
            
            RemoteData.IsInProduction = true;
            IsInProduction.Value = true;
        }

        public void Construct()
        {
            if ((DateTime.Now - RemoteData.LastCommandTime).TotalSeconds <
                Data.ConstructionTime)
            {
                _disposable?.Dispose();
                _disposable = null;
                
                // TODO: Run the Timer and Call OnConstructionFinish at the end.
                _disposable = Observable.Timer(TimeSpan.FromSeconds(1)).Repeat().Subscribe((sec) =>
                {
                    double diff = (DateTime.Now - RemoteData.LastCommandTime).TotalSeconds;

                    ProgressValue.Value = (float) diff / Data.ConstructionTime;

                    if ((float) diff >= Data.ConstructionTime)
                    {
                        _disposable?.Dispose();

                        // TODO: Do this through Service.
                        RemoteData.IsInConstruction = false;

                        // TODO: Do this in Command.
                        IsInConstruction.Value = false;

                        OnConstructionFinish();
                    }
                });
            }
            else
            {
                RemoteData.IsInConstruction = false;
                OnConstructionFinish();
            }
        }

        private void OnConstructionFinish()
        {
            if (Data.AutoProduction)
            {
                Produce();
            }
            
            // TODO: Do this in respective Command after moving stuff to the RemoteCommands.
            _signalBus.Fire<SaveGameSignal>();
        }

        public void Collect()
        {
            if (RemoteData.IsInProduction || RemoteData.IsInConstruction || !RemoteData.IsFull)
            {
                Debug.LogError("Alreading doing somethings");
                return;
            }

            RemoteData.LastCommandTime = DateTime.Now;

            // TODO: Do this through Service.
            RemoteData.IsFull = false;
            
            // TODO: Do this in Command.
            IsFull.Value = false;

            switch (Data.ProductionType)
            {
                case ECurrencyType.Gold:
                    _remoteDataModel.Gold.Value += Data.ProductionPerTimeUnit;
                    _remoteDataModel.UserData.Gold += Data.ProductionPerTimeUnit;
                    break;
                case ECurrencyType.Wood:
                    _remoteDataModel.Wood.Value += Data.ProductionPerTimeUnit;
                    _remoteDataModel.UserData.Wood += Data.ProductionPerTimeUnit;
                    break;
                case ECurrencyType.Iron:
                    _remoteDataModel.Iron.Value += Data.ProductionPerTimeUnit;
                    _remoteDataModel.UserData.Iron += Data.ProductionPerTimeUnit;
                    break;
            }

            if (Data.AutoProduction)
            {
                Produce();
            }
            
            _signalBus.Fire<SaveGameSignal>();
        }

        public bool CanProduce
        {
            get
            {
                return !RemoteData.IsInProduction &&
                       !RemoteData.IsInConstruction &&
                       !RemoteData.IsFull &&
                        Data.ProductionType != ECurrencyType.None;
            }
        }

        public class Factory : PlaceholderFactory<ModuleRemoteDataModel>
        {
        }
    }
}