﻿using game.city.command;
using game.city.installer;
using pg.core.assets;
using UnityEngine;
using Zenject;

namespace game.city.view
{
    public class GamePlayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _prefabContainer;

        [Inject] private ProjectContextInstaller.Settings _settings;

        public override void InstallBindings()
        {
            CreateModulesPool();
            
            Container.DeclareSignal<UpdateModulePositionSignal>().RunSync();
            Container.BindSignal<UpdateModulePositionSignal>().ToMethod<UpdateModulePositionCommand>((x, y) => x.Execute(y)).FromNew();

            Container.DeclareSignal<AddNewModuleSignal>().RunAsync();
            Container.BindSignal<AddNewModuleSignal>().ToMethod<AddNewModuleCommand>((x, y) => x.Execute(y)).FromNew();
            
            Container.BindInterfacesTo<GamePlayMediator>().AsSingle();
        }

        private void CreateModulesPool()
        {
            // Instantiating a new prefab memory pool for Modules
            ModulesPool prefabPool = Container.Instantiate<ModulesPool>(new object[]
                {
                    new MemoryPoolSettings()
                    {
                        MaxSize = 5,
                        InitialSize = 2
                    },
                    //new MemoryPoolSettings(),
                    new ModuleFactory(Container)
                }
            );

            // set parent transform for prefab memory pool
            prefabPool.SetTransformGroup(_prefabContainer);
            Container.BindInstance(prefabPool).AsSingle();
        }
    }
}