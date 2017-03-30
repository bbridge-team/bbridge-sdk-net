using System;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ObjectDetectionData
    {
        #region Constructors

        /// <summary>
        /// Create input structure for Image Object Detection
        /// </summary>
        /// <param name="imageURL">Url of the image to detect from</param>
        /// <param name="threshold">Object detection threshold 0 - 1 </param>
        public ObjectDetectionData(string imageURL, double threshold)
        {
            if (!Uri.IsWellFormedUriString(imageURL, UriKind.Absolute))
            {
                throw new ArgumentException("Image uri is not in a valid format");
            }

			if (threshold < 0 || threshold > 1)
            {
                throw new ArgumentException("Image detection threshold must be with the range 0.01 - 1");
            }

            ImageURL = imageURL;
			DetectionThreshold = threshold;
        }

        #endregion

        #region Properties

        /// <summary>
        /// URL of the image
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string ImageURL { get; set; }

        /// <summary>
        /// Threshold to cut image detection results that confidence is lower than the treshold
        /// </summary>
        [JsonProperty(PropertyName = "threshold")]
        public double DetectionThreshold { get; set; }

        #endregion
    }
}
