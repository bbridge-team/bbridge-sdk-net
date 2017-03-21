using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RequestInfo
    {
        #region Properties

        [JsonProperty(PropertyName = "request_id")]
        public string Id { get; set; }

        #endregion
    }
}
