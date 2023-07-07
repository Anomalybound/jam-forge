using System;
using JamForge;
using JamForge.Serialization;
using JamForge.Store;
using NUnit.Framework;

public class StoreTests
{
    private const string MemoryStoreName = "TestStore";
    private const string PersistStoreName = "TestPersistStore";

    private IJamStores _stores;
    private IPersistStoreVendor _persistStoreVendor;
    private IMemoryStoreVendor _memoryStoreVendor;
    private IStore _memoryStore;
    private IStore _persistStore;

    [SetUp]
    public void SetUp()
    {
        _stores = Jam.Stores;
        _persistStoreVendor = Jam.Stores.Persist;
        _memoryStoreVendor = Jam.Stores.Memory;

        _memoryStore = _memoryStoreVendor.Get(MemoryStoreName);
        _persistStore = _persistStoreVendor.Get(PersistStoreName);
    }

    [TearDown]
    public void TearDown()
    {
        _memoryStoreVendor.Destroy(MemoryStoreName);
        _persistStoreVendor.Destroy(PersistStoreName);

        _memoryStore = null;
        _persistStore = null;
    }

    [Test]
    public void TestStoresNotNull()
    {
        Assert.IsNotNull(Jam.Stores);
    }

    [Test]
    public void TestPersistStoreNotNull()
    {
        Assert.IsNotNull(Jam.Stores.Persist);
    }

    [Test]
    public void TestMemoryStoreNotNull()
    {
        Assert.IsNotNull(Jam.Stores.Memory);
    }

    [Test]
    public void TestStoreVendorCount()
    {
        Assert.AreEqual(1, Jam.Stores.Memory.Count);
        Assert.AreEqual(1, Jam.Stores.Persist.Count);
    }

    [Test]
    public void TestStoreSameResult()
    {
        Assert.AreEqual(Jam.Stores.Memory.Get(MemoryStoreName), _memoryStore);
        Assert.AreEqual(Jam.Stores.Persist.Get(PersistStoreName), _persistStore);
    }

    [Test]
    public void TestStoreVendorHas()
    {
        Assert.IsTrue(Jam.Stores.Memory.Has(MemoryStoreName));
        Assert.IsTrue(Jam.Stores.Persist.Has(PersistStoreName));
    }

    [Test]
    public void TestStoreVendorDestroy()
    {
        Jam.Stores.Memory.Get("TempMemoryStore");
        Jam.Stores.Memory.Destroy("TempMemoryStore");

        Assert.IsFalse(Jam.Stores.Memory.Has("TempMemoryStore"));

        Jam.Stores.Persist.Get("TempPersistStore");
        Jam.Stores.Persist.Destroy("TempPersistStore");

        Assert.IsFalse(Jam.Stores.Persist.Has("TempPersistStore"));
    }

    [Test]
    public void TestStoreValueString()
    {
        const string key = "TestKey";
        const string value = "TestValue";

        _memoryStore.Set(key, value);
        Assert.AreEqual(value, _memoryStore.Get<string>(key));
        Assert.AreEqual(value, Jam.Stores.Memory.Get(MemoryStoreName).Get<string>(key));

        _persistStore.Set(key, value);
        Assert.AreEqual(value, _persistStore.Get<string>(key));
        Assert.AreEqual(value, Jam.Stores.Persist.Get(PersistStoreName).Get<string>(key));
    }

    [Test]
    public void TestStoreValueInt()
    {
        const string key = "TestKey";
        const int value = 2;

        _memoryStore.Set(key, value);
        Assert.AreEqual(value, _memoryStore.Get<int>(key));
        Assert.AreEqual(value, Jam.Stores.Memory.Get(MemoryStoreName).Get<int>(key));

        _persistStore.Set(key, value);
        Assert.AreEqual(value, _persistStore.Get<int>(key));
        Assert.AreEqual(value, Jam.Stores.Persist.Get(PersistStoreName).Get<int>(key));
    }

    [Test]
    public void TestStoreValueFloat()
    {
        const string key = "TestKey";
        const float value = 2.0f;

        _memoryStore.Set(key, value);
        Assert.AreEqual(value, _memoryStore.Get<float>(key));
        Assert.AreEqual(value, Jam.Stores.Memory.Get(MemoryStoreName).Get<float>(key));

        _persistStore.Set(key, value);
        Assert.AreEqual(value, _persistStore.Get<float>(key));
        Assert.AreEqual(value, Jam.Stores.Persist.Get(PersistStoreName).Get<float>(key));
    }

    [Test]
    public void TestStoreValueBool()
    {
        const string key = "TestKey";
        const bool value = true;

        _memoryStore.Set(key, value);
        Assert.AreEqual(value, _memoryStore.Get<bool>(key));
        Assert.AreEqual(value, Jam.Stores.Memory.Get(MemoryStoreName).Get<bool>(key));

        _persistStore.Set(key, value);
        Assert.AreEqual(value, _persistStore.Get<bool>(key));
        Assert.AreEqual(value, Jam.Stores.Persist.Get(PersistStoreName).Get<bool>(key));
    }

    public class TestClass : IEquatable<TestClass>
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public float FloatValue { get; set; }

        public bool BoolValue { get; set; }
        
        public bool Equals(TestClass other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return StringValue == other.StringValue && IntValue == other.IntValue && FloatValue.Equals(other.FloatValue) &&
                   BoolValue == other.BoolValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((TestClass)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StringValue, IntValue, FloatValue, BoolValue);
        }
    }

    [Test]
    public void TestStoreValueObject()
    {
        const string key = "TestKey";
        var value = new TestClass()
        {
            StringValue = "Test",
            IntValue = 2,
            FloatValue = 2.0f,
            BoolValue = true
        };

        _memoryStore.Set(key, value);
        var result = _memoryStore.Get<TestClass>(key);
        Assert.AreEqual(value, result);
        Assert.AreEqual(value, Jam.Stores.Memory.Get(MemoryStoreName).Get<TestClass>(key));

        _persistStore.Set(key, value);
        Assert.AreEqual(value, _persistStore.Get<TestClass>(key));
        Assert.AreEqual(value, Jam.Stores.Persist.Get(PersistStoreName).Get<TestClass>(key));
    }
}
