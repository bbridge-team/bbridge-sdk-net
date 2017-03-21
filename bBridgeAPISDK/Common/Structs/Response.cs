using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{
    public class Response<T>
    {
        #region Properties

        /// <summary>
        /// bBridge API username (provided by bBridge or NExT Center)
        /// </summary>
        [JsonProperty(PropertyName = "requestId")]
        public string RequestId { get; set; }

        //bBridge API password (provided by bBridge or NExT Center)
        [JsonProperty(PropertyName = "content")]
        public T Content { get; set; }

        #endregion
  }
}
