using System;

namespace pg.core.assets
{
    public interface IGenericMemoryPool
    {
        int NumTotal { get; }
        int NumActive { get; }
        int NumInactive { get; }

        Type ItemType
        {
            get;
        }
    }

    public interface IGenericMemoryPool<TValue> : IGenericMemoryPool
    {
        T Spawn<T>() where T : TValue;
        void Despawn(TValue item);
    }

    public interface IGenericMemoryPool<in TParam1, TValue> : IGenericMemoryPool
    {
        T Spawn<T>(TParam1 param) where T : TValue;
        void Despawn(TValue item);
    }

    public interface IGenericMemoryPool<in TParam1, in TParam2, TValue> : IGenericMemoryPool
    {
        T Spawn<T>(TParam1 param1, TParam2 param2) where T : TValue;
        void Despawn(TValue item);
    }

    public interface IGenericMemoryPool<in TParam1, in TParam2, in TParam3, TValue> : IGenericMemoryPool
    {
        T Spawn<T>(TParam1 param1, TParam2 param2, TParam3 param3) where T : TValue;
        void Despawn(TValue item);
    }

    public interface IGenericMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, TValue> : IGenericMemoryPool
    {
        T Spawn<T>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) where T : TValue;
        void Despawn(TValue item);
    }

    public interface IGenericMemoryPool<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, TValue> : IGenericMemoryPool
    {
        T Spawn<T>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) where T : TValue;
        void Despawn(TValue item);
    }
}
