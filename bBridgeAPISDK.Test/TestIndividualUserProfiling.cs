using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.UserProfiling.Individual;
using bBridgeAPISDK.UserProfiling.Individual.Structs;
using bBridgeAPISDKNET.Test;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestIndividualUserProfiling: APIAuthorizedTest
    {
		readonly IndividualUserProfiler individualProfiler;

		public TestIndividualUserProfiling()
        {
            //Wait 60 times 1 seconds each
            individualProfiler = new IndividualUserProfiler(
                new AuthorizedHttpRequester(TestResources.bBridgeAPIBaseURI, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };
        }

        [Fact]
        public async Task TestCanRequestCompleteUserProfileAndReceiveResults()
        {
            var result = await individualProfiler.PredictIndividualUserProfileTask(
                new UserGeneratedContent(
                    new List<string> { "Hello friend!", "The weather is good :)" },
                        new List<string>
                        {
                            "https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg",
                            "https://pbs.twimg.com/media/C6jO3UiVoAQYc_8.jpg"
                        }
                    ),
                new IndividualUserProfilingSettings {
                    AgeGroup = true,
                    EducationLevel = true,
                    Gender = true,
                    IncomeLevel = true,
                    OccupationIndustry = true,
                    RelationshipStatus = true
                });
            
            Assert.False(string.IsNullOrEmpty(result.Profile.Gender));
            Assert.False(string.IsNullOrEmpty(result.Profile.AgeGroup));
            Assert.False(string.IsNullOrEmpty(result.Profile.EducationLevel));
            Assert.False(string.IsNullOrEmpty(result.Profile.IncomeLevel));
            Assert.False(string.IsNullOrEmpty(result.Profile.OccupationIndustry));
            Assert.False(string.IsNullOrEmpty(result.Profile.RelationshipStatus));
        }
    }
}
