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
            var mockResponseListener = new Mock<IResponseListener<Response<IndividualUserProfile>>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsAny<string>(),
                    It.IsNotNull<Response<IndividualUserProfile>>())).
                    Callback(() => responseReceived.Set()).Verifiable();

            var baseUri = new Uri(TestResources.bBridgeAPIBaseURI);

            IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
                TestResources.bBridgeAPIUserName,
                TestResources.bBridgeAPIPassword,
                baseUri);

            var individualProfiler = new IndividualUserProfiler(
                new HttpRequester(baseUri, userPasswordAuthorizer));

            await individualProfiler.RequestIndividuallUserProfiling(
                new UserGeneratedContent(
                    new List<string> { "Hello friend!", "The weather is good :)" },
                    new List<Uri>
                    {
                        new Uri("https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg"),
                        new Uri("https://pbs.twimg.com/media/C6jO3UiVoAQYc_8.jpg")
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


            Assert.True(responseReceived.WaitOne(TimeSpan.FromSeconds(5)));
        }
    }
}
