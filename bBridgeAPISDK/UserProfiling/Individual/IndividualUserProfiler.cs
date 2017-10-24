using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.UserProfiling.Individual.Structs;
using System;

namespace bBridgeAPISDK.UserProfiling.Individual
{
    /// <summary>
    /// Individual User Profiling API capabilities
    /// </summary>
    public class IndividualUserProfiler : APIRequester
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="requester">Any http requester that is descendant of IAsyncHttpRequester</param>
        public IndividualUserProfiler(IAsyncHttpRequester requester) :
            base(requester)
        { }

        /// <summary>
        /// Performs Individual User Profiling Request
        /// </summary>
        /// <param name="ugc">User generated content (texts and/or images) </param>
        /// <param name="settings">Defines which individual attributes to detect</param>
        /// <returns></returns>
        public async Task<IndividualUserProfiling> PredictIndividualUserProfileTask(
            UserGeneratedContent ugc,
            IndividualUserProfilingSettings settings)
        {
            var requestInfo = await RequestAsync(ugc, bBridgeAPIURLSuffixes.IndividualUserProfilingCallSuffix +
                settings.GenerateURLAttributeString());

            return await ReceiveResponseAsync<IndividualUserProfiling>(requestInfo.Id);
        }
    }
}
