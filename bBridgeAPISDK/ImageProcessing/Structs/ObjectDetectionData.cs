using System;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ObjectDetectionData
    {
        #region Constructors
        
        /// <summary>
        /// Create imput structure for Image Object Detection
        /// </summary>
        /// <param name="imageUri">Uri of the image to detect from</param>
        /// <param name="treshold">Object detection treshold 0.01 - 1 </param>
        public ObjectDetectionData(string imageUri, double treshold)
        {
            if (!Uri.IsWellFormedUriString(imageUri, UriKind.Absolute))
            {
                throw new ArgumentException("Image uri is not in a valid format");
            }

            if (treshold < 0.1 || treshold > 1)
            {
                throw new ArgumentException("Image detection treshold must be with the range 0.01 - 1");
            }

            ImageUri = imageUri;
            DetectionTreshold = treshold;
        }

        #endregion

        #region Properties
        /// <summary>
        /// URL of the image
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string ImageUri { get; set; }
        /// <summary>
        /// Treshold to cut image detection results that confidence is lower than the treshold
        /// </summary>
        [JsonProperty(PropertyName = "threshold")]
        public double DetectionTreshold { get; set; }

        #endregion
    }
}