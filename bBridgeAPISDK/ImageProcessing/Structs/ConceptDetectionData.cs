using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
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
        
        [JsonProperty(PropertyName = "image_urls")]
        public IEnumerable<string> ImageURLs { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int TopNumToReturn { get; set; }


        #endregion
    }
}