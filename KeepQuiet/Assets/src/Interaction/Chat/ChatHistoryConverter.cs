using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json.Linq;

public class ChatHistoryConverter : JsonConverter<ChatHistory>
{
    public ChatHistoryConverter() : base()
    {
    }
    public override ChatHistory ReadJson(JsonReader reader, Type objectType,
        ChatHistory existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        // get field data
        string username = (string)obj.SelectToken("m_username");
        List<AssetReference> assetRefs = obj.SelectToken("m_chatLogAssets").
            Select(j =>
            {
                // get guid for each in the list to construct asset referance 
                string guid = j.Value<string>();
                return new AssetReference(guid);
            }
            ).ToList();
        ChatHistory ret = new ChatHistory(username, assetRefs);
        return ret;
    }
    public override void WriteJson(JsonWriter writer, ChatHistory value, 
        JsonSerializer serializer)
    {
        JToken t = JToken.FromObject(value);
        JObject o = (JObject)t;
        o.WriteTo(writer);
    }
}
