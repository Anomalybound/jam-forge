namespace JamForge.Serialization
{
    public interface IJsonSerializer
    {
        string To(object obj);

        T From<T>(string json);
    }
}
