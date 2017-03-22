using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NamedEntities
    {
        [JsonProperty(PropertyName = "results")]
        public List<List<NamedEntity>> NamedEntityLists { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class NamedEntity
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}