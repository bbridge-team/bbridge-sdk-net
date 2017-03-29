using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.ImageProcessing.Structs;

namespace bBridgeAPISDK.ImageProcessing
{
    /// <summary>
    /// Image processing API capabilities
    /// </summary>
    public class ImageProcessor : APIRequester
    {
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
        /// <returns></returns>
        public async Task<ImageObjects> DetectImageObjects(
            ObjectDetectionData imagesData)
        {
            return await ReceiveResponseAsync<ImageObjects>(
                (await RequestAsync(imagesData, bBridgeAPIURLSuffixes.ObjectDetectionSuffix)).Id);
        }
        /// <summary>
        /// Performs Image concept detection API request
        /// </summary>
        /// <param name="imagesData">Images to detect concepts from</param>
        /// <returns></returns>
        public async Task<ImageConcepts> DetectImageConcepts(
            ConceptDetectionData imagesData)
        {
            return await ReceiveResponseAsync<ImageConcepts>(
                (await RequestAsync(imagesData, bBridgeAPIURLSuffixes.ConceptDetectionSuffix)).Id);
        }
    }
}
