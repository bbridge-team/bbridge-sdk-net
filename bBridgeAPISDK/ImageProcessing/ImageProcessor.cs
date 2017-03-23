using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.ImageProcessing.Structs;

namespace bBridgeAPISDK.ImageProcessing
{
    /// <summary>
    /// Image processing API capabilities
    /// </summary>
    public class ImageProcessor : APIFeature
    {
        private const string objectDetectionSuffix = "image/objects";
        private const string conceptDetectionSuffix = "image/concepts";

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="requester">Any http requester that is descendant of IAsyncHttpRequester</param>
        public ImageProcessor(IAsyncHttpRequester requester) :
            base(requester)
        { }

        /// <summary>
        /// Performs Object detection API request
        /// </summary>
        /// <param name="imagesData">Image to detect objects from</param>
        /// <param name="responseListener">Request response listener (via callback)</param>
        /// <returns></returns>
        public async Task<string> RequestImageObjectDetection(
            ObjectDetectionData imagesData,
            IResponseListener<ImageObjects> responseListener)
        {
            var requestID = (await obtainRequestID(imagesData, objectDetectionSuffix)).Id;

            RequestAsyncAndWaitForResponseInCallback(requestID, responseListener);

            return requestID;
        }
        /// <summary>
        /// Performs Image concept detection API request
        /// </summary>
        /// <param name="imagesData">Images to detect concepts from</param>
        /// <param name="responseListener">Request response listener (via callback)</param>
        /// <returns></returns>
        public async Task<string> RequestImageConceptDetection(
            ConceptDetectionData imagesData,
            IResponseListener<ImageConcepts> responseListener)
        {
            var requestID = (await obtainRequestID(imagesData, conceptDetectionSuffix)).Id;

            RequestAsyncAndWaitForResponseInCallback(requestID, responseListener);

            return requestID;
        }
    }
}
