using System;
using System.Collections.Generic;
using System.Text;
using bBridgeAPISDK.Common.Structs;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageConcepts
    {
        [JsonProperty(PropertyName = "results")]
        public List<ImageConceptDistribution> ImageConceptDistributions { get; set; }

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageConceptDistribution
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage Error { get; set; }

        [JsonProperty(PropertyName = "concepts", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Concepts { get; set; }
    }
}