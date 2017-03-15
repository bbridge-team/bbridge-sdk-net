using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Interfaces;
using bBridgeAPISDK.Profiling.Individual.Structs;
using bBridgeAPISDK.Structs;
using bBridgeAPISDK.Utils.Interfaces;

namespace bBridgeAPISDK.Profiling.Individual
{
    public class IndividualUserProfiler: APIFeature
    {
        private const string bBridgeApiProfilingSuffix = "profiling/personal?";

        public IndividualUserProfiler(IAsyncHttpRequester requester):
            base(requester){}

        public async Task<KeyValuePair<string, UserGeneratedContent>> RequestIndividuallUserProfiling(
            UserGeneratedContent ugc,
            IndividualUserProfilingSettings settings,
            IResponseListener<Response<IndividualUserProfile>> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, bBridgeApiProfilingSuffix + settings.GenerateURLAttributeString())).Id;
            
            waitForResponseAsync(requestID, responseListener);

            return new KeyValuePair<string, UserGeneratedContent>(requestID, ugc);
        }
    }
}
