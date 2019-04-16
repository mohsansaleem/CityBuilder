using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using RSG;
using Zenject;

namespace pg.core.assets
{
    public abstract class KeyedAsyncGenericMemoryPoolBase<TKey, TContract> : IGenericMemoryPool
    {
        Dictionary<TKey, Stack<TContract>> _inactiveItems;
        IAsyncGenericAssetFactory<TKey, TContract> _factory;
        MemoryPoolSettings _settings;
        DiContainer _container;

        Dictionary<TKey, int> _activeCount;
        
        [Inject]
        void Construct(
            IAsyncGenericAssetFactory<TKey, TContract> factory,
            DiContainer container,
            [InjectOptional]
            MemoryPoolSettings settings)
        {
            // MemoryPoolSettings InitialSize affects stack initial size
            _settings = settings ?? MemoryPoolSettings.Default;
            _factory = factory;
            _container = container;

            _inactiveItems = new Dictionary<TKey, Stack<TContract>>();
            _activeCount = new Dictionary<TKey, int>();

        }

        public IDictionary<TKey, Stack<TContract>> InactiveItems
        {
            get { return _inactiveItems; }
        }

        public int NumTotal
        {
            get { return NumInactive + NumActive; }
        }

        public int NumTypes
        {
            get { return _inactiveItems.Count; }
        }

        public int NumInactive
        {
            get { return _inactiveItems.Sum(kvp => kvp.Value.Count); }
        }

        public int NumActive
        {
            get { return _activeCount.Sum(kvp => kvp.Value); }
        }

        public Type ItemType
        {
            get { return typeof(TContract); }
        }
        
        
        public int StackActive(TKey key)
        {
            return _activeCount.ContainsKey(key) ? _activeCount[key]: 0;
        }
        
        public int StackTotal(TKey key)
        {
            return StackInactive(key) + StackTotal(key);
        }

        public int StackInactive(TKey key)
        {
            return _inactiveItems.ContainsKey(key) ? _inactiveItems[key].Count : 0;
        }
        
        public void Despawn<T>(TKey key, TContract item) where T : TContract
        {
            Assert.That(!_inactiveItems[key].Contains(item),
                "Tried to return an item to pool {0} twice", this.GetType());

            _activeCount[key]--;

            _inactiveItems[key].Push(item);


            // TODO: MS: Add timer thingy for disposing.
            //DisposeExpiredInactiveItems();

            OnDespawned(key, item);
            
            if (_inactiveItems[key].Count > _settings.MaxSize)
            {
                Resize<T>(key, _settings.MaxSize);
            }
        }

        IPromise<T> AllocNew<T>(TKey key) where T : TContract
        {
            try
            {
                IPromise<T> promise = _factory.Create<T>(key)
                    .Then(
                        (item) =>
                        {
                            Assert.IsNotNull(item, "Factory '{0}' returned null value when creating via {1}!",
                                _factory.GetType(), this.GetType());

                            try
                            {
                                OnCreated(key, item);
                            }
                            catch (Exception e)
                            {
                                Log.ErrorException("Pool: " + string.Format("Error during construction of type {0}", typeof(TContract)), e);
                            }
                        });

                return promise;
            }
            catch (Exception e)
            {
                throw new ZenjectException(
                    "Error during construction of type '{0}' via {1}.Create method!".Fmt(
                        typeof(TContract), this.GetType().Name), e);
            }
        }
        
        public IPromise Clear<T>(TKey key) where T : TContract
        {
            return Resize<T>(key,0);
        }

        public IPromise ShrinkBy<T>(TKey key, int numToRemove) where T : TContract
        {
            return Resize<T>(key,_inactiveItems[key].Count - numToRemove);
        }

        public IPromise ExpandBy<T>(TKey key, int numToAdd) where T : TContract
        {
            return Resize<T>(key,StackInactive(key) + numToAdd);
        }

        protected IPromise<T> GetInternal<T>(TKey key) where T : TContract
        {
            Promise<T> promise = new Promise<T>();

            if (!_inactiveItems.ContainsKey(key) || _inactiveItems[key].IsEmpty())
            {
                ExpandPool<T>(key).Then(() =>
                    {
                        Assert.That(!_inactiveItems[key].IsEmpty());
                        promise.Resolve((T) _inactiveItems[key].Pop());
                    },
                    promise.Reject);
            }
            else
            {
                promise.Resolve((T)_inactiveItems[key].Pop());
            }

            return promise
                .Then(item =>
                {
                    _activeCount[key]++;
                    OnSpawned(key,item);
                });
        }

        public IPromise CreatePool<T>(TKey key) where T : TContract
        {
            Promise promise = new Promise();
            
            if (!_inactiveItems.ContainsKey(key))
            {
                Stack<TContract> newStack = new Stack<TContract>(_settings.InitialSize);
                Promise<T> tmpPromise = null;
                
                if (!_container.IsValidating)
                {
                    _activeCount.Add(key, 0);
                    _inactiveItems.Add(key, newStack);
                    
                    for (int i = 0; i < _settings.InitialSize; i++)
                    {
                        if (tmpPromise == null)
                        {
                            tmpPromise = (Promise<T>)AllocNew<T>(key)
                                .Then(item =>
                                {
                                    _inactiveItems[key].Push(item);
                                    //_pushedToPoolTimestamps[key].Add(DateTime.Now);
                                });
                        }
                        else
                        {
                            tmpPromise = (Promise<T>)tmpPromise.Then((o) =>
                            {
                                AllocNew<T>(key)
                                    .Then(item =>
                                    {
                                        _inactiveItems[key].Push(item);
                                        //_pushedToPoolTimestamps[key].Add(DateTime.Now);
                                    });
                            });
                        }
                    }
                }

                if (tmpPromise != null)
                {
                    tmpPromise.Done(o =>
                    {
                        promise.Resolve();
                    },promise.Reject);
                }
                else
                {
                    promise.Resolve();
                }
            }
            else
            {
                promise.Resolve();
            }
            
            return promise;
        }
        
        public IPromise Resize<T>(TKey key, int desiredPoolSize) where T : TContract
        {
            Promise promise = new Promise();
            
            if (StackInactive(key) == desiredPoolSize)
            {
                promise.Resolve();
            }

            if (_settings.ExpandMethod == PoolExpandMethods.Disabled)
            {
                promise.Reject(new PoolExceededFixedSizeException(
                    "Pool factory '{0}' attempted resize but pool set to fixed size of '{1}'!"
                        .Fmt(GetType(), StackInactive(key))));
            }
            else
            {
                Assert.That(desiredPoolSize >= 0, "Attempted to resize the pool to a negative amount");

                while (_inactiveItems[key].Count > desiredPoolSize)
                {
                    OnDestroyed(key, _inactiveItems[key].Pop());
                }

                Promise<T> tmpSeq = null;
                
                for(int i = _inactiveItems[key].Count; i < desiredPoolSize; i++)
                {
                    if (tmpSeq == null)
                    {
                        tmpSeq = (Promise<T>)AllocNew<T>(key)
                            .Then(item =>
                            {
                                _inactiveItems[key].Push(item);
                            });
                    }
                    else
                    {
                        tmpSeq = (Promise<T>)(tmpSeq.Then((o) =>
                                    {
                                        AllocNew<T>(key)
                                            .Then(item =>
                                            {
                                                _inactiveItems[key].Push(item);
                                            });
                                    }));
                    }
                }

                if (tmpSeq != null)
                {
                    tmpSeq.Done(tmp =>
                    {
                        Assert.IsEqual(_inactiveItems[key].Count, desiredPoolSize);
                        
                        promise.Resolve();
                    }, promise.Reject);
                }
                else
                {
                    promise.Resolve();
                }
                
            }

            return promise;
        }

        public IPromise ExpandPool<T>(TKey key) where T : TContract
        {
            Promise promise = new Promise();

            CreatePool<T>(key).Done(() =>
            {
                switch (_settings.ExpandMethod)
                {
                    case PoolExpandMethods.Disabled:
                    {
                        promise.Reject(new PoolExceededFixedSizeException(
                            "Pool factory '{0}' exceeded its max size of '{1}'!"
                                .Fmt(this.GetType(), StackTotal(key))));
                        break;
                    }
                    case PoolExpandMethods.OneAtATime:
                    {
                        ExpandBy<T>(key, 1)
                            .Done(()=>promise.Resolve());
                        break;
                    }
                    case PoolExpandMethods.Double:
                    {
                        if (StackTotal(key) == 0)
                        {
                            ExpandBy<T>(key, 1)
                                .Done(()=>promise.Resolve());
                        }
                        else
                        {
                            ExpandBy<T>(key, StackTotal(key))
                                .Done(()=>promise.Resolve());
                        }
                        break;
                    }
                    default:
                    {
                        promise.Reject(Assert.CreateException());
                        break;
                    }
                }
            });
            
            
            
            return promise;
        }

        protected virtual void OnDespawned(TKey key, TContract item)
        {
            // Optional
        }

        protected virtual void OnSpawned(TKey key, TContract item)
        {
            // Optional
        }

        protected virtual void OnCreated(TKey key, TContract item)
        {
            // Optional
        }
        
        protected virtual void OnDestroyed(TKey key, TContract item)
        {
            // Optional
        }
    }
}
