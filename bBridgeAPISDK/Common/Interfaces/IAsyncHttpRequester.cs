using System;
using System.Threading.Tasks;

namespace bBridgeAPISDK.Utils.Interfaces
{
    public interface IAsyncHttpRequester
    {
        Uri BaseAdress { get; }
        Task<T> RequestAsync<T>(string urlSuffix, object data=null) where T : class;
    }
}
