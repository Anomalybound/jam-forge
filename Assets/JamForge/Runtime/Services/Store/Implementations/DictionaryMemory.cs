using System.Collections.Generic;
using JamForge.Serialization;
using JetBrains.Annotations;
using VContainer;

namespace JamForge.Store
{
    [UnityEngine.Scripting.Preserve]
    public class InMemoryDictionaryStore : IMemoryStoreVendor
    {
        public int Count => _stores.Count;

        private readonly IObjectResolver _resolver;
        private readonly Dictionary<string, DictionaryMemory> _stores;

        public IStore Get(string storeName)
        {
            if (_stores.TryGetValue(storeName, out var store))
            {
                return store;
            }

            store = _resolver.Resolve<DictionaryMemory>();
            _stores[storeName] = store;

            return store;
        }

        public bool Has(string storeName)
        {
            return _stores.ContainsKey(storeName);
        }

        public void Destroy(IStore store)
        {
            if (store is not DictionaryMemory memory) { return; }

            _stores.Remove(memory.GetType().Name);
            memory.DeleteAll();
        }

        [UsedImplicitly]
        private class DictionaryMemory : IStore
        {
            private readonly IBinarySerializer _binarySerializer;
            private readonly Dictionary<string, byte[]> _store;

            public DictionaryMemory(IBinarySerializer binarySerializer)
            {
                _binarySerializer = binarySerializer;
                _store = new Dictionary<string, byte[]>();
            }

            public void Set<T>(string key, T value)
            {
                _store[key] = _binarySerializer.To(value);
            }

            public T Get<T>(string key)
            {
                if (_store.TryGetValue(key, out var bytes))
                {
                    return _binarySerializer.From<T>(bytes);
                }

                throw new KeyNotFoundException($"Key {key} not found");
            }

            public bool Has<T>(string key)
            {
                return _store.ContainsKey(key);
            }

            public bool TryGet<T>(string key, out T value)
            {
                if (_store.TryGetValue(key, out var bytes))
                {
                    value = _binarySerializer.From<T>(bytes);
                    return true;
                }

                value = default;
                return false;
            }

            public bool Delete<T>(string key)
            {
                return _store.Remove(key);
            }

            public void DeleteAll()
            {
                _store.Clear();
            }
        }
    }
}
