using Newtonsoft.Json;

namespace bBridgeAPISDK.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AuthorizationToken
    {
        #region Properties

        /// <summary>
        /// API authorization token
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        #endregion
    }
}
