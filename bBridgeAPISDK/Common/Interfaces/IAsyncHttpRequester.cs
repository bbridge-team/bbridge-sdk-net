using System;
using System.Threading.Tasks;

namespace bBridgeAPISDK.Common.Interfaces
{
    /// <summary>
    /// Http Requester - provide basic http
    /// requests with embeded JSON serialization capabilities
    /// </summary>
    public interface IAsyncHttpRequester
    {
        /// <summary>
        /// Base Adress of the HTTP server
        /// </summary>
        string BaseAdress { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Result deserialization type</typeparam>
        /// <param name="urlSuffix">Http server URL suffix</param>
        /// <param name="data">Data for POST request</param>
        /// <returns></returns>
        Task<T> RequestAsync<T>(string urlSuffix, object data=null) where T : class;
    }
}
