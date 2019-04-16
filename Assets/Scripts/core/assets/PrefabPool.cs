using RSG;

namespace pg.core.assets
{
    public class PrefabPool : KeyedAsyncGenericMonoMemoryPool<FactoryObject, FactoryObjectParams, FactoryObject>
    {
        protected override void Reinitialize<T>(FactoryObjectParams assetParams, FactoryObject item, Promise<T> assetReadyPromise)
        {
            item.Reinitialize(assetParams, assetReadyPromise);
        }

        protected override void OnCreated(FactoryObject prefab, FactoryObject item)
        {
            base.OnCreated(prefab, item);

            item.OnCreated();
        }

        protected override void OnSpawned(FactoryObject prefab, FactoryObject item)
        {
            base.OnSpawned(prefab, item);

            item.OnSpawned();
        }

        protected override void OnDespawned(FactoryObject prefab, FactoryObject item)
        {
            base.OnDespawned(prefab, item);

            item.OnDespawned();
        }
    }
}
