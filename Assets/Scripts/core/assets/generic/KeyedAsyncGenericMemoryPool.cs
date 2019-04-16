using System;
using ModestTree;
using RSG;

namespace pg.core.assets
{
    // Zero parameters
    public class KeyedAsyncGenericMemoryPool<TKey, TValue> : KeyedAsyncGenericMemoryPoolBase<TKey, TValue>,
        IKeyedAsyncGenericMemoryPool<TKey, TValue>
    {
        public IPromise<T> Spawn<T>(TKey key) where T : TValue
        {
            Promise<T> assetReadyPromise = new Promise<T>();
            GetInternal<T>(key)
                .Then(
                    item => TryReinitialize(key, item, assetReadyPromise),
                    exception => assetReadyPromise.Reject(exception));
            return assetReadyPromise;
        }

        protected void TryReinitialize<T>(TKey key, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            try
            {
                Reinitialize(item, assetReadyPromise);
            }
            catch (Exception e)
            {
                Log.ErrorException("Error during pool item Reinitialize", e);

                if (assetReadyPromise.CurState == PromiseState.Pending)
                {
                    assetReadyPromise.Resolve((T)item);
                }
            }
        }

        protected virtual void Reinitialize<T>(TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            // Optional
            assetReadyPromise.Resolve((T)item);
        }
    }

    // One parameter
    public class KeyedAsyncGenericMemoryPool<TKey, TParam1, TValue> : KeyedAsyncGenericMemoryPoolBase<TKey, TValue>,
        IKeyedAsyncGenericMemoryPool<TKey, TParam1, TValue>
    {
        public IPromise<T> Spawn<T>(TKey key, TParam1 param) where T : TValue
        {
            Promise<T> assetReadyPromise = new Promise<T>();
            GetInternal<T>(key)
                .Then(
                    item => TryReinitialize(key, param, item, assetReadyPromise),
                    exception => assetReadyPromise.Reject(exception));
            return assetReadyPromise;
        }

        protected void TryReinitialize<T>(TKey key, TParam1 p1, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            try
            {
                Reinitialize(p1, item, assetReadyPromise);
            }
            catch (Exception e)
            {
                Log.ErrorException("Error during pool item Reinitialize", e);

                if (assetReadyPromise.CurState == PromiseState.Pending)
                {
                    assetReadyPromise.Resolve((T)item);
                }
            }
        }

        protected virtual void Reinitialize<T>(TParam1 p1, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            // Optional
            assetReadyPromise.Resolve((T)item);
        }
    }

    // Two parameters
    public class KeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TValue> : KeyedAsyncGenericMemoryPoolBase<TKey, TValue>,
        IKeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TValue>
    {
        public IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2) where T : TValue
        {
            Promise<T> assetReadyPromise = new Promise<T>();
            GetInternal<T>(key)
                .Then(
                    item => TryReinitialize(key, param1, param2, item, assetReadyPromise),
                    exception => assetReadyPromise.Reject(exception));
            return assetReadyPromise;
        }

        protected void TryReinitialize<T>(TKey key, TParam1 p1, TParam2 p2, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            try
            {
                Reinitialize(p1, p2, item, assetReadyPromise);
            }
            catch (Exception e)
            {
                Log.ErrorException("Error during pool item Reinitialize", e);

                if (assetReadyPromise.CurState == PromiseState.Pending)
                {
                    assetReadyPromise.Resolve((T)item);
                }
            }
        }

        protected virtual void Reinitialize<T>(TParam1 p1, TParam2 p2, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            // Optional
            assetReadyPromise.Resolve((T)item);
        }
    }

    // Three parameters
    public class KeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TParam3, TValue> : KeyedAsyncGenericMemoryPoolBase<TKey, TValue>,
        IKeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TParam3, TValue>
    {
        public IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3) where T : TValue
        {
            Promise<T> assetReadyPromise = new Promise<T>();
            GetInternal<T>(key)
                .Then(
                    item => TryReinitialize(key, param1, param2, param3, item, assetReadyPromise),
                    exception => assetReadyPromise.Reject(exception));
            return assetReadyPromise;
        }

        protected void TryReinitialize<T>(TKey key, TParam1 p1, TParam2 p2, TParam3 p3, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            try
            {
                Reinitialize(p1, p2, p3, item, assetReadyPromise);
            }
            catch (Exception e)
            {
                Log.ErrorException("Error during pool item Reinitialize", e);

                if (assetReadyPromise.CurState == PromiseState.Pending)
                {
                    assetReadyPromise.Resolve((T)item);
                }
            }
        }

        protected virtual void Reinitialize<T>(TParam1 p1, TParam2 p2, TParam3 p3, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            // Optional
            assetReadyPromise.Resolve((T)item);
        }
    }

    // Four parameters
    public class KeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TParam3, TParam4, TValue> :
        KeyedAsyncGenericMemoryPoolBase<TKey, TValue>, IKeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TParam3, TParam4, TValue>
    {
        public IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) where T : TValue
        {
            Promise<T> assetReadyPromise = new Promise<T>();
            GetInternal<T>(key)
                .Then(
                    item => TryReinitialize(key, param1, param2, param3, param4, item, assetReadyPromise),
                    exception => assetReadyPromise.Reject(exception));
            return assetReadyPromise;
        }

        protected void TryReinitialize<T>(TKey key, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            try
            {
                Reinitialize(p1, p2, p3, p4, item, assetReadyPromise);
            }
            catch (Exception e)
            {
                Log.ErrorException("Error during pool item Reinitialize", e);

                if (assetReadyPromise.CurState == PromiseState.Pending)
                {
                    assetReadyPromise.Resolve((T)item);
                }
            }
        }

        protected virtual void Reinitialize<T>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            // Optional
            assetReadyPromise.Resolve((T)item);
        }
    }

    // Five parameters
    public class KeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TParam3, TParam4, TParam5, TValue> :
        KeyedAsyncGenericMemoryPoolBase<TKey, TValue>,
        IKeyedAsyncGenericMemoryPool<TKey, TParam1, TParam2, TParam3, TParam4, TParam5, TValue>
    {
        public IPromise<T> Spawn<T>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4,
            TParam5 param5) where T : TValue
        {
            Promise<T> assetReadyPromise = new Promise<T>();
            GetInternal<T>(key)
                .Then(
                    item => TryReinitialize(key, param1, param2, param3, param4, param5, item, assetReadyPromise),
                    exception => assetReadyPromise.Reject(exception));
            return assetReadyPromise;
        }

        protected void TryReinitialize<T>(TKey key, TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            try
            {
                Reinitialize(p1, p2, p3, p4, p5, item, assetReadyPromise);
            }
            catch (Exception e)
            {
                Log.ErrorException("Error during pool item Reinitialize", e);

                if (assetReadyPromise.CurState == PromiseState.Pending)
                {
                    assetReadyPromise.Resolve((T)item);
                }
            }
        }

        protected virtual void Reinitialize<T>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TValue item, Promise<T> assetReadyPromise) where T : TValue
        {
            // Optional
            assetReadyPromise.Resolve((T)item);
        }
    }
}
