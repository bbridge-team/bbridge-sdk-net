using System;
using System.Threading.Tasks;
using bBridgeAPISDK.Interfaces;
using bBridgeAPISDK.Structs;
using bBridgeAPISDK.Utils.Interfaces;

namespace bBridgeAPISDK
{
    public abstract class APIFeature
    {
        #region Fields

        protected const string apiResponseSuffix = "response?id=";

        private const int responseWaitTimeMilliseconds = 500;
        private const int responseWaitNumAttempts = 10;

        protected readonly IAsyncHttpRequester requester;

        #endregion

        #region Constructors

        protected APIFeature(IAsyncHttpRequester requester)
        {
            if (requester == null)
            {
                throw new ArgumentException("Requester must be specified in order to make API calls");
            }

            this.requester = requester;

            ResponseWaitTime = TimeSpan.FromMilliseconds(responseWaitTimeMilliseconds);
            ResponseWaitNumAttempts = responseWaitNumAttempts;
        }

        #endregion

        #region Properties

        public TimeSpan ResponseWaitTime { get; set; }

        public int ResponseWaitNumAttempts { get; set; }

        #endregion

        #region Methods

        protected async Task<RequestInfo> obtainRequestID(UserGeneratedContent ugc, string suffix)
        {
            return await requester.RequestAsync<RequestInfo>(suffix, ugc);
        }

        protected async void waitForResponseAsync<T>(string requestId, IResponseListener<T> responseListener) where T : class
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
