using System.Collections.Generic;
using JamForge.Serialization;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace JamForge.Store
{
    [UnityEngine.Scripting.Preserve]
    public class PlayerPrefsStoreVendor : IPersistStoreVendor
    {
        public int Count => _stores.Count;

        private readonly IObjectResolver _resolver;
        private readonly Dictionary<string, PlayerPrefsPersistStore> _stores;

        public PlayerPrefsStoreVendor(IObjectResolver resolver)
        {
            _resolver = resolver;
            _stores = new Dictionary<string, PlayerPrefsPersistStore>();
        }

        public IStore Get(string storeName)
        {
            if (_stores.TryGetValue(storeName, out var store))
            {
                return store;
            }

            store = _resolver.Resolve<PlayerPrefsPersistStore>();
            store.StoreName = storeName;
            _stores[storeName] = store;

            return store;
        }

        public bool Has(string storeName)
        {
            return _stores.ContainsKey(storeName);
        }

        public void Destroy(IStore store)
        {
            if (store is not PlayerPrefsPersistStore persistStore) { return; }

            _stores.Remove(persistStore.StoreName);
            persistStore.DeleteAll();
        }

        [UsedImplicitly]
        private class PlayerPrefsPersistStore : IStore
        {
            private readonly IJsonSerializer _json;

            private readonly Dictionary<string, string> _keyCaches;

            private readonly List<string> _keys;

            public PlayerPrefsPersistStore(IJsonSerializer json)
            {
                _json = json;
                _keyCaches = new Dictionary<string, string>();
                _keys = new List<string>();
            }

            public string StoreName { get; set; }

            private string ProcessKey(string key)
            {
                if (_keyCaches.TryGetValue(key, out var cache))
                {
                    return cache;
                }

                cache = $"{StoreName}_{key}";
                _keyCaches[key] = cache;
                return cache;
            }

            public void Set<T>(string key, T value)
            {
                key = ProcessKey(key);

                if (!_keys.Contains(key)) { _keys.Add(key); }

                PlayerPrefs.SetString(key, _json.To(value));
                PlayerPrefs.Save();
            }

            public T Get<T>(string key)
            {
                key = ProcessKey(key);
                return _json.From<T>(PlayerPrefs.GetString(key));
            }

            public bool Has<T>(string key)
            {
                key = ProcessKey(key);
                return PlayerPrefs.HasKey(key);
            }

            public bool TryGet<T>(string key, out T value)
            {
                key = ProcessKey(key);
                if (Has<T>(key))
                {
                    value = Get<T>(key);
                    return true;
                }

                value = default;
                return false;
            }

            public bool Delete<T>(string key)
            {
                key = ProcessKey(key);
                if (!Has<T>(key)) { return false; }

                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
                return true;

            }

            public void DeleteAll()
            {
                foreach (var key in _keys)
                {
                    PlayerPrefs.DeleteKey(key);
                }

                PlayerPrefs.Save();
            }
        }
    }
}
