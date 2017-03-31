using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Authorization.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Credentials
    {
        #region Properties

        /// <summary>
        /// bBridge API username (provided by bBridge or NExT Center)
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string User { get; set; }


        /// <summary>
        /// bBridge API password (provided by bBridge or NExT Center)
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        #endregion
    }
}
