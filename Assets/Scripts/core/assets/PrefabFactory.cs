using RSG;
using UnityEngine;
using Zenject;

namespace pg.core.assets
{
    public class FactoryObjectParams
    {
        public string AssetDataKey;
    }

    public abstract class FactoryObject : MonoBehaviour
    {
        //T will always be the class inheriting FactoryObject.
        //To properly implement Reinitialize:
        //1. Wait for any internal promises to resolve (i.e. loading a sub asset that you plan to child to this one)
        //2. Resolve assetReadyPromise by calling assetReadyPromise.Resolve(this as T);
        public abstract void Reinitialize<T>(FactoryObjectParams assetParams, Promise<T> assetReadyPromise) where T : FactoryObject;
        public virtual void OnCreated() { }
        public virtual void OnSpawned() { }
        public virtual void OnDespawned() { }
    }
    
    public class PrefabFactory : IAsyncGenericAssetFactory<FactoryObject, FactoryObject>
    {
        private readonly DiContainer _container;

        public PrefabFactory(DiContainer container)
        {
            _container = container;
        }

        public IPromise<T> Create<T>(FactoryObject prefab) where T : FactoryObject
        {
            Promise<T> createAssetPromise = new Promise<T>();
            GameObject instance = _container.InstantiatePrefab(prefab);
            T factoryObject = instance.GetComponent<T>() ?? _container.InstantiateComponent<T>(instance);
            createAssetPromise.Resolve(factoryObject);
            return createAssetPromise;
        }
    }
}
