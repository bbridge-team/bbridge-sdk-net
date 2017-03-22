using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PartOfSpeechTags
    {
        [JsonProperty(PropertyName = "results")]
        public List<List<PartOfSpeechTag>> PartOfSpeechTagLists { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PartOfSpeechTag
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}