using System;
using System.Threading.Tasks;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.Common.Structs;

namespace bBridgeAPISDK.Common
{
    /// <summary>
    /// Base class for all API features
    /// </summary>
    public abstract class APIFeature
    {
        #region Fields
        ///
        protected const string apiResponseSuffix = "response?id=";

        private const int responseWaitTimeMilliseconds = 500;
        private const int responseWaitNumAttempts = 10;

        protected readonly IAsyncHttpRequester requester;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for basic class
        /// </summary>
        /// <param name="requester">Http requester descendant</param>
        protected APIFeature(IAsyncHttpRequester requester)
        {
            if (requester == null)
            {
                throw new ArgumentException("requester must be specified in order to make API calls");
            }
            
            this.requester = requester;

            ResponseWaitTime = TimeSpan.FromMilliseconds(responseWaitTimeMilliseconds);
            ResponseWaitNumAttempts = responseWaitNumAttempts;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Time to wait for response before rerequest
        /// </summary>
        public TimeSpan ResponseWaitTime { get; set; }

        //Number of re-requests
        public int ResponseWaitNumAttempts { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initiate a request
        /// </summary>
        /// <param name="ugc">data to be sent in POST request</param>
        /// <param name="urlSuffix">API feature URL suffix</param>
        /// <returns></returns>
        protected async Task<RequestInfo> obtainRequestID(object ugc, string urlSuffix)
        {
            return await requester.RequestAsync<RequestInfo>(urlSuffix, ugc);
        }

        /// <summary>
        /// Requests the response for the requestId to be called back via responseListener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestId">Previously made request id</param>
        /// <param name="responseListener">Response callback listener</param>
        public async void RequestAsyncAndWaitForResponseInCallback<T>(string requestId, IResponseListener<T> responseListener) where T : class
        {
            var numAttempts = ResponseWaitNumAttempts;
            T response = null;

            while (numAttempts-- > 0 && response == null)
            {
                await Task.Delay(ResponseWaitTime);
                        
                response = await requester.RequestAsync<T>(apiResponseSuffix + requestId);
            }

            responseListener.ResponseReceived(requestId, response);
        }

        #endregion
    }
}
