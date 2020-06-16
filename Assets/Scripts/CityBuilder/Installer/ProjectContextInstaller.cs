using System;
using System.Collections.Generic;
using PG.City.Command;
using PG.City.Context.Bootstrap;
using PG.City.Context.Gameplay;
using PG.City.Model;
using PG.City.Model.data;
using PG.City.Model.remote;
using PG.City.Model.scene;
using PG.Core.Installer;
using PG.service;
using UnityEngine;
using Zenject;

namespace PG.City.Installer
{
    public class ProjectContextInstaller : CoreContextInstaller
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.Bind<StaticDataModel>().AsSingle();
            Container.Bind<RemoteDataModel>().AsSingle();
            Container.Bind<BootstrapModel>().AsSingle();
            Container.Bind<GamePlayModel>().AsSingle();
            
            Container.DeclareSignal<SaveGameSignal>().RunAsync();
            Container.BindSignal<SaveGameSignal>().ToMethod<SaveGameCommand>((x, y) => x.Execute(y)).FromNew();
            
            Container.BindFactory<ModuleRemoteDataModel, ModuleRemoteDataModel.Factory>();
            
            // We can bind different Service here like Remote server.
            // For now I am using local plain files.
            Container.BindInterfacesTo<FileStorageService>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            // Meta Stuff
            public DefaultGameState DefaultGameState;
            
            /// <summary>
            /// Just doing it for the ease of creating MetaData from Scriptable object.
            /// </summary>
            public DefaultMetaData MetaDataAsset;
            
            public List<BuildingPrefab> BuildingsPrefabs;
        }   
    }
    
    [Serializable]
    public class BuildingPrefab
    {
        public EModuleType Type;
        public GameObject Prefab;

        public override string ToString()
        {
            return Type+"s";
        }
    }
}