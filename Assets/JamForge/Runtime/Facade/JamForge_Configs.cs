using JamForge.Store;
using VContainer;

namespace JamForge
{
    public partial class Jam
    {
        [Inject] private IPersistStoreVendor _persistStoreVendor;
        [Inject] private IMemoryStoreVendor _memoryStoreVendor;

        public static IPersistStoreVendor PersistStores => Instance._persistStoreVendor;
        public static IMemoryStoreVendor MemoryStores => Instance._memoryStoreVendor;
    }
}
