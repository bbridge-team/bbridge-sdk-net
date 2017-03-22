using System.Collections.Generic;
using bBridgeAPISDK.Common.Structs;
using Newtonsoft.Json;

namespace bBridgeAPISDK.ImageProcessing.Structs
{

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageObjects
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorMessage Error { get; set; }

        [JsonProperty(PropertyName = "objects", NullValueHandling = NullValueHandling.Ignore)]
        public List<ImageObject> Objects { get; set; }

    }


    [JsonObject(MemberSerialization.OptIn)]
    public class ImageObject
    {
        [JsonProperty(PropertyName = "cls_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }

        [JsonProperty(PropertyName = "x")]
        public double LeftTopCornerX { get; set; }

        [JsonProperty(PropertyName = "y")]
        public double LeftTopCornerY { get; set; }

        [JsonProperty(PropertyName = "w")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "h")]
        public double Height { get; set; }

    }
}
