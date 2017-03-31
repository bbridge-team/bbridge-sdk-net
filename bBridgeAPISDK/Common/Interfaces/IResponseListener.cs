namespace bBridgeAPISDK.Common.Interfaces
{
    /// <summary>
    /// Any class that will listen for API responces must be derived from this interface
    /// </summary>
    /// <typeparam name="T">Type of the response message</typeparam>
    public interface IResponseListener<in T>
    {
        /// <summary>
        /// API responce callback
        /// </summary>
        /// <param name="requestId">initial request id</param>
        /// <param name="response">Response data of type T</param>
        void ResponseReceived(string requestId, T response);
    }
}
