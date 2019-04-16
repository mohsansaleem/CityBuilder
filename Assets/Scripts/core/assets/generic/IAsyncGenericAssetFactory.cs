using RSG;

namespace pg.core.assets
{
    public interface IAsyncGenericAssetFactory
    {
    }

    public interface IAsyncGenericAssetFactory<in TKey, TContract> : IAsyncGenericAssetFactory
    {
        IPromise<T> Create<T>(TKey key) where T : TContract;
    }
    
    public interface IAsyncGenericAssetFactory<in TKey, in TParam1, TContract> : IAsyncGenericAssetFactory
    {
        IPromise<T> Create<T>(TKey key, TParam1 param) where T : TContract;
    }

    public interface IAsyncGenericAssetFactory<in TKey, in TParam1, in TParam2, TContract> : IAsyncGenericAssetFactory
    {
        IPromise<T> Create<T>(TKey key, TParam1 param1, TParam2 param2) where T : TContract;
    }

    public interface IAsyncGenericAssetFactory<in TKey, in TParam1, in TParam2, in TParam3, TContract> : IAsyncGenericAssetFactory
    {
        IPromise<T> Create<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3) where T : TContract;
    }
}
