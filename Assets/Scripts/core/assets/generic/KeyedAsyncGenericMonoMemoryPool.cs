using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace pg.core.assets
{
    public abstract class KeyedAsyncGenericMonoMemoryPool<TKey, TParam1, TValue> : KeyedAsyncGenericMemoryPool<TKey, TParam1, TValue>
        where TValue : Component
    {
        private Transform _parentGroup;
        private Dictionary<TKey, Transform> _transformGroup;

        public void SetTransformGroup(Transform parent)
        {
            _parentGroup = parent;
            _transformGroup = new Dictionary<TKey, Transform>();
        }

        protected override void OnCreated(TKey key, TValue item)
        {
            item.gameObject.SetActive(false);

            if (!_transformGroup.ContainsKey(key))
            {
                CreateParentContainer(key);
            }

            item.transform.SetParent(_transformGroup[key], false);
        }

        protected override void OnSpawned(TKey key, TValue item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(TKey key, TValue item)
        {
            item.gameObject.SetActive(false);

            if (!_transformGroup.ContainsKey(key))
            {
                Log.Warn("Pool", string.Format("Stack not found for despawn with key {0}, item {1}", key, item));

                // in case we've dumped the pool due to memory
                CreateParentContainer(key);
            }
            
            item.transform.SetParent(_transformGroup[key], false);
        }

        protected override void OnDestroyed(TKey key, TValue item)
        {
            GameObject.Destroy(item.gameObject);
        }

        private void CreateParentContainer(TKey key)
        {
            GameObject newStack = new GameObject();
            newStack.transform.SetParent(_parentGroup, false);
            newStack.name = key.ToString();
            newStack.SetActive(false);
            _transformGroup.Add(key, newStack.transform);
        }
    }
}
