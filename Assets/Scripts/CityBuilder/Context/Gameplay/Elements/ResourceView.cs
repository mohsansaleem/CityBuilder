using PG.Core.PoolFactory;
using RSG;

namespace PG.CityBuilder.Context.Gameplay
{
    public class ResourceView : ModuleView
    {        
        public override void Reinitialize<T>(FactoryObjectParams assetParams, Promise<T> assetReadyPromise)
        { 
            // Do the thing
            
            base.Reinitialize(assetParams, assetReadyPromise);
            
        }

        public override void OnDespawned()
        {
            base.OnDespawned();
        }

    }
}