using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.Common.Structs;
using bBridgeAPISDK.ImageProcessing.Structs;

namespace bBridgeAPISDK.ImageProcessing
{
    public class ImageProcessor : APIFeature
    {
        private const string objectDetectionSuffix = "image/objects";
        private const string conceptDetectionSuffix = "image/concepts";

        public ImageProcessor(IAsyncHttpRequester requester) :
            base(requester)
        { }

        public async Task<string> RequestImageObjectDetection(
            ObjectDetectionData ugc,
            IResponseListener<ImageObjects> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, objectDetectionSuffix)).Id;

            waitForResponseAsync(requestID, responseListener);

            return requestID;
        }

        public async Task<string> RequestImageConceptDetection(
            ConceptDetectionData ugc,
            IResponseListener<ImageConcepts> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, conceptDetectionSuffix)).Id;

            waitForResponseAsync(requestID, responseListener);

            return requestID;
        }
    }
}
