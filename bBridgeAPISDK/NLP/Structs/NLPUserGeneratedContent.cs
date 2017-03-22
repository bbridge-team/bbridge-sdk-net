using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NLPUserGeneratedContent
    {
        [JsonProperty(PropertyName = "sentences")]
        public List<string> Sentences { get; set; }
    }
}
