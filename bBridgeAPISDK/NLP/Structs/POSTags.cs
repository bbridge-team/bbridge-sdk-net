using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    /// <summary>
    /// Structure to be returned as a result of Part Of SpeechDetection API method
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PartOfSpeechTags
    {
        /// <summary>
        /// Parts of speech detected form the input text
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<List<PartOfSpeechTag>> PartOfSpeechTagLists { get; set; }
    }

    /// <summary>
    /// Internal structure of Part of speech detection
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class PartOfSpeechTag
    {
        /// <summary>
        /// Fragment of the input text
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Detected POS type
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
