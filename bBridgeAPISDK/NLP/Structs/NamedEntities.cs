using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    /// <summary>
    /// Structure to be returned as a result of Named Entity Recognition API method
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class NamedEntities
    {
        /// <summary>
        /// Lists of named entities detected from the given text
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<List<NamedEntity>> NamedEntityLists { get; set; }
    }

    /// <summary>
    /// Internal structure of Image Concept detection
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class NamedEntity
    {
        /// <summary>
        /// Number of results
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Fragment of the input text
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Detected named entity
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}