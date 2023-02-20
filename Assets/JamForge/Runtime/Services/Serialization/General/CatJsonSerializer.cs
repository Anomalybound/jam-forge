using System;
using CatJson;
using UnityEngine.Scripting;

namespace JamForge.Serialization
{
    [Preserve]
    public class CatJsonSerializer : IJsonSerializer
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
}
