using System.Collections.Generic;
using bBridgeAPISDK.Common.Structs;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{
    /// <summary>
    /// Structure to be returned as a result of Image Object Detection API method
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageObjects
    {
        /// <summary>
        /// Error messahe if request went wrong. Null if request is succesfull
        /// </summary>
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage Error { get; set; }

        /// <summary>
        /// Detected objects from an image
        /// </summary>
        [JsonProperty(PropertyName = "objects", NullValueHandling = NullValueHandling.Ignore)]
        public List<ImageObject> Objects { get; set; }

    }

    /// <summary>
    /// Internal structure of Image Object detection
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageObject
    {
        /// <summary>
        /// Name of the object
        /// </summary>
        [JsonProperty(PropertyName = "cls_name")]
        public string Name { get; set; }

        /// <summary>
        /// Model confidence of the detection result
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }

        /// <summary>
        /// Left top corner X coordinate of the detected object
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public double LeftTopCornerX { get; set; }

        /// <summary>
        /// Left top corner Y coordinate of the detected object
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public double LeftTopCornerY { get; set; }

        /// <summary>
        /// Width of the detected object
        /// </summary>
        [JsonProperty(PropertyName = "w")]
        public double Width { get; set; }

        //Height of the detected object
        [JsonProperty(PropertyName = "h")]
        public double Height { get; set; }

    }
}
