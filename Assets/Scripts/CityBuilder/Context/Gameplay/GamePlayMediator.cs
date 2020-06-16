using System;
using System.Collections.Generic;
using System.Linq;
using PG.City.Installer;
using PG.City.Model;
using PG.City.Model.Data;
using PG.City.Model.Remote;
using PG.City.Model.Context;
using PG.Core.FSM;
using RSG;
using UniRx;
using UnityEngine;
using Zenject;

namespace PG.City.Context.Gameplay
{
    public partial class GamePlayMediator : StateMachineMediator
    {
        [Inject] public ProjectContextInstaller.Settings _projectSettings;
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly GamePlayView _view;
        [Inject] private Camera _camera;

        [Inject] private readonly BootstrapModel _bootstrapModel;
        [Inject] private readonly GamePlayModel _gamePlayModel;
        [Inject] private readonly StaticDataModel _staticDataModel;
        [Inject] private readonly RemoteDataModel _remoteDataModel;

        [Inject] private readonly ModulesPool _buildingsesPool;

        // GameObjects
        private Dictionary<long, ModuleView> _buildingViews = new Dictionary<long, ModuleView>();
        private ModuleView _placeholderModule;
        
        // Variables
        private Vector3 _destination;
        
        // Module Events.
        private Action<EntityRemoteDataModel> _onMouseDownAtModule;
        private Action<EntityRemoteDataModel> _onDragModule;
        private Action<EntityRemoteDataModel> _onMouseUpFromModule;

        // Test
        private IDisposable o;
        
        public override void Initialize()
        {
            base.Initialize();

            StateBehaviours.Add(typeof(GamePlayStateLoad), new GamePlayStateLoad(this));
            StateBehaviours.Add(typeof(GamePlayStateRegular), new GamePlayStateRegular(this));
            StateBehaviours.Add(typeof(GamePlayStateBuild), new GamePlayStateBuild(this));

            // Observing Remote Data.
            _remoteDataModel.ModuleRemoteDatas.ObserveAdd().Subscribe(OnModuleAdd).AddTo(Disposables);
            _remoteDataModel.ModuleRemoteDatas.ObserveRemove().Subscribe(OnModuleRemove).AddTo(Disposables);

            _remoteDataModel.Gold.Subscribe(gold => _view.GoldText.text = gold.ToString()).AddTo(Disposables);
            _remoteDataModel.Wood.Subscribe(wood => _view.WoodText.text = wood.ToString()).AddTo(Disposables);
            _remoteDataModel.Iron.Subscribe(iron => _view.IronText.text = iron.ToString()).AddTo(Disposables);
            
            _view.RegularModeButton.onClick.AddListener(() => 
                _gamePlayModel.GamePlayState.Value = GamePlayModel.EGamePlayState.Regular);
            
            _view.BuildModeButton.onClick.AddListener(() => 
                _gamePlayModel.GamePlayState.Value = GamePlayModel.EGamePlayState.Build);
            
            // Adding all the Modules. Costly and lame but works.
            _view.ModuleSelectionDropDown.AddOptions(GetModulesList());

            // Add Module Listner.
            _view.AddModule.onClick.AddListener(()=> AddNewModuleSignal.AddModule(SignalBus,
                (EModuleType)_view.ModuleSelectionDropDown.value));
            
            OnGamePlayStateChanged(GamePlayModel.EGamePlayState.Load);
            
            _gamePlayModel.GamePlayState.Subscribe(OnGamePlayStateChanged).AddTo(Disposables);
        }


        private void OnModuleAdd(DictionaryAddEvent<long, ModuleRemoteDataModel> obj)
        {
            SpawnModuleAndAdd(obj.Value).Catch(exception => Debug.LogError(exception));
        }

        private IPromise<ModuleView> SpawnModuleAndAdd(ModuleRemoteDataModel moduleModel)
        {
            return SpawnModule(moduleModel).Catch(exception => Debug.LogError(exception))
                // Adding it to the Views Dictionary.
                .Then(v => _buildingViews.Add(moduleModel.RemoteData.Id, v));
        }

        private void OnModuleRemove(DictionaryRemoveEvent<long, ModuleRemoteDataModel> obj)
        {
            DeSpawnModule(obj.Value);
        }

        private IPromise<ModuleView> SpawnModule(ModuleRemoteDataModel moduleModel)
        {
            return _buildingsesPool.Spawn<ModuleView>(
                _projectSettings.BuildingsPrefabs.First(a => a.Type.Equals(moduleModel.RemoteData.moduleType)),
                new ModuleViewParams()
                {
                    parent = _view.ModulesRoot,
                    ModuleModel = moduleModel
                })
                // Setting Actions to the Module View.
                .Then(m =>
                {
                    m.OnMouseDownAtModule = OnMouseDowned;
                    m.OnDragModule = OnModuleDragged;
                    m.OnMouseUpFromModule += OnMouseUp;
                });
        }
        
        private void DeSpawnModule(ModuleRemoteDataModel moduleModel)
        {
            _buildingsesPool.Despawn<ModuleView>(
                _projectSettings.BuildingsPrefabs.First(a => a.Type.Equals(moduleModel.RemoteData.moduleType)),
                _buildingViews[moduleModel.RemoteData.Id]);
            
            _buildingViews.Remove(moduleModel.RemoteData.Id);
        }

        private ModuleView GetModule(ModuleRemoteDataModel model)
        {
            return _buildingViews.ContainsKey(model.RemoteData.Id) ? 
                _buildingViews[model.RemoteData.Id] :
                null;
        }
        
        private void OnMouseDowned(EntityRemoteDataModel model)
        {
            _onMouseDownAtModule?.Invoke(model);
        }
        
        private void OnModuleDragged(EntityRemoteDataModel model)
        {
            _onDragModule?.Invoke(model);
        }

        private void OnMouseUp(EntityRemoteDataModel model)
        {
            _onMouseUpFromModule?.Invoke(model);
        }

        public List<string> GetModulesList()
        {
            return Enum.GetValues(typeof(EModuleType)).Cast<EModuleType>().
                Select(x => x + ": " + _staticDataModel.GetModuleData(x).GetCostString())
                .ToList();
        }
        
        private void OnGamePlayStateChanged(GamePlayModel.EGamePlayState gamePlayState)
        {
            Type targetType = null;
            switch (gamePlayState)
            {
                case GamePlayModel.EGamePlayState.Load:
                    targetType = typeof(GamePlayStateLoad);
                    break;
                case GamePlayModel.EGamePlayState.Regular:
                    targetType = typeof(GamePlayStateRegular);
                    break;
                case GamePlayModel.EGamePlayState.Build:
                    targetType = typeof(GamePlayStateBuild);
                    break;
            }

            if (targetType != null &&
                (CurrentStateBehaviour == null ||
                 targetType != CurrentStateBehaviour.GetType()))
            {
                GoToState(targetType);

                // Setting Values.
                _view.GameModeText.text = gamePlayState.ToString();
                _view.AddModulePanel.gameObject.SetActive(gamePlayState == GamePlayModel.EGamePlayState.Build);
            }
        }
    }
}