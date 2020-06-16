using System;
using PG.CityBuilder.Installer;
using PG.CityBuilder.Model;
using PG.CityBuilder.Model.Context;
using PG.CityBuilder.Model.Remote;
using PG.Core.FSM;
using PG.Core.Installer;
using UniRx;
using UnityEngine;
using Zenject;

namespace PG.CityBuilder.Context.Bootstrap
{
    public partial class BootstrapMediator : StateMachineMediator
    {
        [Inject] private readonly BootstrapView _view;
        
        [Inject] private readonly BootstrapModel _bootstrapModel;

        [Inject] private readonly StaticDataModel _staticDataModel;
        [Inject] private readonly RemoteDataModel _remoteDataModel;

        [Inject] private ProjectContextInstaller.Settings _gameSettings;

        public BootstrapMediator()
        {
            Disposables = new CompositeDisposable();
        }

        public override void Initialize()
        {
            base.Initialize();

            StateBehaviours.Add(typeof(BootstrapMediator.BootstrapStateLoadStaticData), new BootstrapStateLoadStaticData(this));
            StateBehaviours.Add(typeof(BootstrapStateCreateMetaData), new BootstrapStateCreateMetaData(this)); // Temporary State for creating MetaData
            StateBehaviours.Add(typeof(BootstrapStateLoadUserData), new BootstrapStateLoadUserData(this));
            StateBehaviours.Add(typeof(BootstrapStateCreateUserData), new BootstrapStateCreateUserData(this));
            StateBehaviours.Add(typeof(BootstrapStateGamePlay), new BootstrapStateGamePlay(this));
            
            _bootstrapModel.LoadingProgress.Subscribe(OnLoadingProgressChanged).AddTo(Disposables);
        }

        private void OnLoadingProgressChanged(BootstrapModel.ELoadingProgress loadingProgress)
        {
            _view.ProgressBar.value = (float)loadingProgress / 100;


            Type targetType = null;
            switch (loadingProgress)
            {
                case BootstrapModel.ELoadingProgress.Zero:
                    targetType = typeof(BootstrapStateLoadStaticData);
                    break;
                case BootstrapModel.ELoadingProgress.MetaNotFound:
                    targetType = typeof(BootstrapStateCreateMetaData);
                    break;
                case BootstrapModel.ELoadingProgress.StaticDataLoaded:
                    targetType = typeof(BootstrapStateLoadUserData);
                    break;
                case BootstrapModel.ELoadingProgress.UserNotFound:
                    targetType = typeof(BootstrapStateCreateUserData);
                    break;
                case BootstrapModel.ELoadingProgress.DataSeeded:
                    targetType = typeof(BootstrapStateGamePlay);
                    break;
            }

            if (targetType != null &&
                (CurrentStateBehaviour == null ||
                 targetType != CurrentStateBehaviour.GetType()))
            {
                GoToState(targetType);
            }
        }

        private void OnReload()
        {
            _view.Show();

            UnloadAllScenesExceptSignal.UnloadAllExcept(SignalBus, Scenes.Bootstrap).Done
            (
                () =>
                {
                    _bootstrapModel.LoadingProgress.Value = BootstrapModel.ELoadingProgress.Zero;
                },
                exception =>
                {
                    Debug.LogError("Error While Reloading: " + exception);
                }
            );
        }

        private void OnLoadingStart()
        {
            _view.Show();
        }
    }
}

