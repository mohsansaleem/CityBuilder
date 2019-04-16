using game.city.installer;
using RSG;

namespace pg.core.assets
{
    public class ModulesPool : KeyedAsyncGenericMonoMemoryPool<BuildingPrefab, FactoryObjectParams, FactoryObject>
    {
        protected override void Reinitialize<T>(FactoryObjectParams assetParams, FactoryObject item, Promise<T> assetReadyPromise)
        {
            item.Reinitialize(assetParams, assetReadyPromise);
        }

        protected override void OnCreated(BuildingPrefab key, FactoryObject item)
        {
            base.OnCreated(key, item);

            item.OnCreated();
        }

        protected override void OnSpawned(BuildingPrefab key, FactoryObject item)
        {
            base.OnSpawned(key, item);
            item.OnSpawned();
        }

        protected override void OnDespawned(BuildingPrefab key, FactoryObject item)
        {
            base.OnDespawned(key, item);

            item.OnDespawned();
        }
    }
}
