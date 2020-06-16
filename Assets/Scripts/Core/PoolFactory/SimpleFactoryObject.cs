using RSG;

namespace PG.Core.PoolFactory
{
    public class SimpleFactoryObject : FactoryObject
    {
        private FactoryObjectParams _assetParams;
        public string AssetDataKey
        {
            get { return _assetParams.AssetDataKey; }
        }

        public override void Reinitialize<T>(FactoryObjectParams assetParams, Promise<T> assetReadyPromise)
        {
            _assetParams = assetParams;
            assetReadyPromise.Resolve(this as T);
        }
    }

}
