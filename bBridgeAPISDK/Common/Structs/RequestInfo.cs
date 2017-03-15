using Newtonsoft.Json;

namespace bBridgeAPISDK.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RequestInfo
    {
        #region Properties

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        #endregion
    }
}
