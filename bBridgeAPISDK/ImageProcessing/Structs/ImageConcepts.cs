using System.Collections.Generic;
using bBridgeAPISDK.Common.Structs;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
    /// <summary>
    /// Structure to be returned as a result of Image Concept Detection API method
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageConcepts
    {
        /// <summary>
        /// Distributions among image concepts in the order given in request
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<ImageConceptDistribution> ImageConceptDistributions { get; set; }

    }

    /// <summary>
    /// Internal structure of Image Concept detection
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageConceptDistribution
    {
        /// <summary>
        /// Error messahe if request went wrong. Null if request is succesfull
        /// </summary>
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage Error { get; set; }

        /// <summary>
        /// Concept distribution
        /// </summary>
        [JsonProperty(PropertyName = "concepts", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, double> Concepts { get; set; }
    }
}