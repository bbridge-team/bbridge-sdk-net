using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
    /// <summary>
    /// Structure to be send in Object Detection request
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ConceptDetectionData
    {
        #region Constructors

        /// <summary>
        /// Creates the structure with the specified image for Image Concept Detection API endpoint
        /// </summary>
        /// <param name="topNumToReturn">Number of image concepts to return</param>
        /// <param name="imageURLs">URLs of the images from user's profile</param>
        public ConceptDetectionData(IEnumerable<string> imageURLs, int topNumToReturn)
        {
            ImageURLs = imageURLs;
            TopNumToReturn = topNumToReturn;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Image URLS to be detected from
        /// </summary>
        [JsonProperty(PropertyName = "image_urls")]
        public IEnumerable<string> ImageURLs { get; set; }

        /// <summary>
        /// Number of Objects top TopNumToReturn detected objects to return (based on model confidence)
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int TopNumToReturn { get; set; }

        #endregion
    }
}