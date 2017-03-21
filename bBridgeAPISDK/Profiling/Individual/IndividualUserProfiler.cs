using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.Common.Structs;
using bBridgeAPISDK.Profiling.Individual.Structs;

namespace bBridgeAPISDK.Profiling.Individual
{
    public class IndividualUserProfiler: APIFeature
    {
        private const string bBridgeApiProfilingSuffix = "profiling/personal?";

        public IndividualUserProfiler(IAsyncHttpRequester requester):
            base(requester){}

        public async Task<string> RequestIndividuallUserProfiling(
            UserGeneratedContent ugc,
            IndividualUserProfilingSettings settings,
            IResponseListener<Response<IndividualUserProfile>> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, bBridgeApiProfilingSuffix + settings.GenerateURLAttributeString())).Id;

            waitForResponseAsync(requestID, responseListener);

            return requestID;
        }
    }
}
