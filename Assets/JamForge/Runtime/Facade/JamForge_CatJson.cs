using System;
using CatJson;
using VContainer;

namespace JamForge
{
    public class JsonWrapper
    {
        public string To(JsonObject jsonObject)
        {
            return jsonObject.ToJson();
        }

        public string To(object obj)
        {
            return JsonParser.Default.ToJson(obj);
        }

        public T From<T>(string json)
        {
            return JsonParser.Default.ParseJson<T>(json);
        }

        public object From(string json, Type type)
        {
            return JsonParser.Default.ParseJson(json, type);
        }
    }

    public partial class Jam
    {
        private readonly JsonWrapper _json = new();

        public static JsonWrapper Json => Instance._json;
    }
}
