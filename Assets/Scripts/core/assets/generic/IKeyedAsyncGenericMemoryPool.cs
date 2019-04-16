using RSG;

namespace pg.core.assets
{
    public interface IKeyedAsyncGenericMemoryPool<in TKey, TValue> : IGenericMemoryPool
    {
        IPromise<T> Spawn<T>(TKey key) where T : TValue;
        void Despawn<T>(TKey key, TValue item) where T : TValue;
    }

    public interface IKeyedAsyncGenericMemoryPool<in TKey, in TParam1, TValue> : IGenericMemoryPool
    {
        IPromise<T> Spawn<T>(TKey key, TParam1 param) where T : TValue;
        void Despawn<T>(TKey key, TValue item) where T : TValue;
    }

    public interface IKeyedAsyncGenericMemoryPool<in TKey, in TParam1, in TParam2, TValue> : IGenericMemoryPool
    {
        IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2) where T : TValue;
        void Despawn<T>(TKey key, TValue item) where T : TValue;
    }

    public interface IKeyedAsyncGenericMemoryPool<in TKey, in TParam1, in TParam2, in TParam3, TValue> : IGenericMemoryPool
    {
        IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3) where T : TValue;
        void Despawn<T>(TKey key, TValue item) where T : TValue;
    }

    public interface IKeyedAsyncGenericMemoryPool<in TKey, in TParam1, in TParam2, in TParam3, in TParam4,
        TValue> : IGenericMemoryPool
    {
        IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) where T : TValue;
        void Despawn<T>(TKey key, TValue item) where T : TValue;
    }

    public interface IKeyedAsyncGenericMemoryPool<in TKey, in TParam1, in TParam2, in TParam3, in TParam4, in TParam5,
        TValue> : IGenericMemoryPool
    {
        IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4,
            TParam5 param5) where T : TValue;

        void Despawn<T>(TKey key, TValue item) where T : TValue;
    }
}
