using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.ImageProcessing;
using bBridgeAPISDK.ImageProcessing.Structs;
using Moq;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestImageProcessing
    {
		readonly IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
				TestResources.bBridgeAPIUserName,
				TestResources.bBridgeAPIPassword,
				TestResources.bBridgeAPIBaseURI);

		readonly ImageProcessor imageProcessor;


		public TestImageProcessing()
        {
            //Wait 60 times 1 seconds each
            imageProcessor = new ImageProcessor(new AuthorizedHttpRequester(TestResources.bBridgeAPIBaseURI, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };
        }

        [Fact]
        public async Task TestCanRequestObjectDetectionAndReceiveResultsInCallback()
        {
            var responseReceived = new AutoResetEvent(false);
            var mockResponseListener = new Mock<IResponseListener<ImageObjects>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsNotNull<string>(),
                    It.IsNotNull<ImageObjects>())).
                    Callback<string, ImageObjects>((id, objects) =>
                    {
                        //Signaling that the result was called back
                        responseReceived.Set();

                        //Check if all attributes are not empty and not null
                        Assert.False(string.IsNullOrEmpty(id));
                        Assert.InRange(objects.Objects.Count, 1, 101);

                        foreach(var obj in objects.Objects)
                        {
                            Assert.False(string.IsNullOrEmpty(obj.Name));
                            Assert.InRange(obj.Score, 0.00001, 1);
                        }
                    }).Verifiable();

            await imageProcessor.RequestImageObjectDetection(
                new ObjectDetectionData("https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg", 0.5),
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }

        [Fact]
        public async Task TestCanRequestConceptDetectionAndReceiveResultsInCallback()
        {
            var responseReceived = new AutoResetEvent(false);
            var mockResponseListener = new Mock<IResponseListener<ImageConcepts>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsNotNull<string>(),
                    It.IsNotNull<ImageConcepts>())).
                    Callback<string, ImageConcepts>((id, concepts) =>
                    {
                        //Signaling that the result was called back
                        responseReceived.Set();

                        //Check if all attributes are not empty and not null
                        Assert.False(string.IsNullOrEmpty(id));
                        Assert.Equal(concepts.ImageConceptDistributions.Count, 2);
                        foreach (var distribution in concepts.ImageConceptDistributions)
                        {
                            Assert.Equal(distribution.Concepts.Count, 5);

                            foreach (var concept in distribution.Concepts)
                            {
                                Assert.False(string.IsNullOrEmpty(concept.Key));
                                Assert.InRange(concept.Value, 0.000001, 1);
                            }

                        }
                        //Assert.False(string.IsNullOrEmpty(concepts.Objects[0].Name));
                    }).Verifiable();

            await imageProcessor.RequestImageConceptDetection(
                new ConceptDetectionData(
                    new List<string>
                        {
                            "https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg",
                            "https://pbs.twimg.com/media/C6jO3UiVoAQYc_8.jpg"
                        }, 5), 
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }
    }
}
