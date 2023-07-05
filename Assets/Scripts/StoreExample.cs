using System;
using JamForge;
using UnityEngine;

public class StoreExample : MonoBehaviour
{
    private void Start()
    {
        var store = Jam.MemoryStores.Get("TestStore");
        // store.Set("TestKey", "TestValue");
        Jam.Logger.Debug(store.TryGet<string>("TestKey", out var value) ? value : "No value found for TestKey");

        var persistStore = Jam.PersistStores.Get("TestPersistStore");
        // persistStore.Set("TestKey", "TestValue");
        Jam.Logger.Debug(persistStore.TryGet("TestKey", out value) ? value : "No value found for TestKey");
    }
}
