using pg.core.assets;
using RSG;

namespace game.city.view
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