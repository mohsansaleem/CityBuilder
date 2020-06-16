using PG.Core.thirdparty.RSG.Promise.v1._3._0._0;

namespace PG.City.Context.Gameplay
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