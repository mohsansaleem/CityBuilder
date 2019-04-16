using game.city.installer;
using UnityEngine;
using Zenject;
using RSG;

namespace pg.core.assets
{
    public class ModuleFactory : IAsyncGenericAssetFactory<BuildingPrefab, FactoryObject>
    {
        private readonly DiContainer _container;
        
        public ModuleFactory(DiContainer container)
        {
            _container = container;
        }
        
        public IPromise<T> Create<T>(BuildingPrefab prefabData) where T : FactoryObject
        {
            Promise<T> createAssetPromise = new Promise<T>();
            GameObject instance = _container.InstantiatePrefab(prefabData.Prefab);
            T factoryObject = instance.GetComponent<T>() ?? _container.InstantiateComponent<T>(instance);
            createAssetPromise.Resolve(factoryObject);
            return createAssetPromise;
        }
    }
}
