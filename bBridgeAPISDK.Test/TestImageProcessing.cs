using System;
using System.Threading;
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
        [Fact]
        public async void TestCanRequestCompleteObjectDetectionAndReceiveResultsInCallback()
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
                        Assert.False(string.IsNullOrEmpty(objects.Objects[0].Name));
                    }).Verifiable();

            var baseUri = new Uri(TestResources.bBridgeAPIBaseURI);

            IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
                TestResources.bBridgeAPIUserName,
                TestResources.bBridgeAPIPassword,
                baseUri);

            //Wait 60 times 1 seconds each
            var imageProcessor = new ImageProcessor(
                new HttpRequester(baseUri, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };

            await imageProcessor.RequestImageObjectDetection(
                new ObjectDetectionData("https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg", 0.5),
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }
    }
}
