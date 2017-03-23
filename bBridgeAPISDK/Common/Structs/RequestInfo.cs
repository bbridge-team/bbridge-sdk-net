using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{
    /// <summary>
    /// Information about made request structure
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RequestInfo
    {
        #region Properties

        /// <summary>
        /// Id of the made request
        /// </summary>
        [JsonProperty(PropertyName = "request_id")]
        public string Id { get; set; }

        #endregion
    }
}
