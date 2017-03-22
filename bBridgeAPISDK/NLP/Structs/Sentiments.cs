using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Sentiments
    {
        [JsonProperty(PropertyName = "results")]
        public List<double> SentimentsList { get; set; }
    }

}