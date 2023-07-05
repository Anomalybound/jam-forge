using Nino.Serialization;
using UnityEngine.Scripting;

namespace JamForge.Serialization
{
    [Preserve]
    public class NaniBinarySerializer : IBinarySerializer
    {
        public byte[] To(object obj, bool isCompressed = false)
        {
            return Serializer.Serialize(obj, isCompressed ? CompressOption.Zlib : CompressOption.NoCompression);
        }

        public T From<T>(byte[] bytes, bool isCompressed = false)
        {
            return Deserializer.Deserialize<T>(bytes, isCompressed ? CompressOption.Zlib : CompressOption.NoCompression);
        }
    }
}
