using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{

    [JsonObject(MemberSerialization.OptIn)]
    public class ErrorMessage
    {
        [JsonProperty(PropertyName = "error")]
        public string Message { get; set; }
    }
}
