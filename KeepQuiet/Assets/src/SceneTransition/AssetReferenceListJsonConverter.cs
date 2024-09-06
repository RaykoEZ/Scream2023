using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json.Linq;
public class AssetReferenceListJsonConverter : JsonConverter
{
    public AssetReferenceListJsonConverter() : base()
    {
    }
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<string>);
    }
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        List<AssetReference> ret = new List<AssetReference>();
        JObject obj = JObject.Load(reader);
        foreach (var guid in obj)
        {
            ret.Add(new AssetReference(guid.Value.ToString()));
        }
        return ret;
    }
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var assetRefs = value as List<AssetReference>;
        writer.WriteStartArray();
        foreach (var player in assetRefs)
        {
            writer.WriteValue(JsonConvert.SerializeObject(player.AssetGUID));
        }
        writer.WriteEndArray();
    }
}
