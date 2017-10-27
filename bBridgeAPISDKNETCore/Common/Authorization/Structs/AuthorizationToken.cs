using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Authorization.Structs
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
