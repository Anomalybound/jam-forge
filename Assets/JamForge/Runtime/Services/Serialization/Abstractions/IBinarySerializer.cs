namespace JamForge.Serialization
{
    public interface IBinarySerializer
    {
        byte[] To(object obj, bool isCompressed = false);
        
        T From<T>(byte[] bytes, bool isCompressed = false);
    }
}
