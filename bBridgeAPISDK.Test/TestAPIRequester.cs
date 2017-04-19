using System;
using System.Threading;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.ImageProcessing.Structs;
using bBridgeAPISDKNET.Test;
using Moq;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestAPIRequester : APIAuthorizedTest
    {
        [Fact]
        public async void TestCanRequestObjectDetectionAndReceiveResultsInCallback()
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
                        Assert.InRange(objects.Objects.Count, 1, 101);

                        foreach (var obj in objects.Objects)
                        {
                            Assert.False(string.IsNullOrEmpty(obj.Name));
                            Assert.InRange(obj.Score, 0.00001, 1);
                        }
                    }).Verifiable();

            //Wait 60 times 1 seconds each
            var apiRequester = new APIRequester(new AuthorizedHttpRequester(TestResources.bBridgeAPIBaseURI, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };

            apiRequester.ReceiveResponseAsync((await apiRequester.RequestAsync(
                new ObjectDetectionData("https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg", 0.5),
                bBridgeAPIURLSuffixes.ObjectDetectionSuffix)).Id, mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }
    }
}
