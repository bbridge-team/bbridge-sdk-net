using System;
using System.Collections.Generic;
using System.Threading;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.Common.Structs;
using bBridgeAPISDK.Profiling.Individual;
using bBridgeAPISDK.Profiling.Individual.Structs;
using Moq;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestIndividualUserProfiling
    {   
        [Fact]
        public async void TestCanRequestCompleteUserProfileAndReceiveResultsInCallback()
        {
            var responseReceived = new AutoResetEvent(false);
            var mockResponseListener = new Mock<IResponseListener<IndividualUserProfiling>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsNotNull<string>(),
                    It.IsNotNull<IndividualUserProfiling>())).
                    Callback<string, IndividualUserProfiling>((id, profile) =>
                {
                    //Signaling that the result was called back
                    responseReceived.Set();

                    //Check if all attributes are not empty and not null
                    Assert.False(string.IsNullOrEmpty(id));
                    Assert.False(string.IsNullOrEmpty(profile.Profile.Gender));
                    Assert.False(string.IsNullOrEmpty(profile.Profile.AgeGroup));
                    Assert.False(string.IsNullOrEmpty(profile.Profile.EducationLevel));
                    Assert.False(string.IsNullOrEmpty(profile.Profile.IncomeLevel));
                    Assert.False(string.IsNullOrEmpty(profile.Profile.OccupationIndustry));
                    Assert.False(string.IsNullOrEmpty(profile.Profile.RelationshipStatus));
                }).Verifiable();

            var baseUri = new Uri(TestResources.bBridgeAPIBaseURI);

            IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
                TestResources.bBridgeAPIUserName,
                TestResources.bBridgeAPIPassword,
                baseUri);

            //Wait 60 times 1 seconds each
            var individualProfiler = new IndividualUserProfiler(
                new HttpRequester(baseUri, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };

            await individualProfiler.RequestIndividuallUserProfiling(
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
                },
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }
    }
}
