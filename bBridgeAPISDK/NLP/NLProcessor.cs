using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Enums;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.NLP.Structs;

namespace bBridgeAPISDK.NLP
{
    /// <summary>
    /// NLP Processing API capabilities
    /// </summary>
    public class NLProcessor : APIFeature
    {
        private const string posTagProcessingSuffix = "nlp/pos?";
        private const string sentimentAnalysisSuffix = "nlp/sentiment?";
        private const string nerProcessingSuffix = "nlp/ner?";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requester">Any http requester that is descendant of IAsyncHttpRequester</param>
        public NLProcessor(IAsyncHttpRequester requester) :
            base(requester)
        { }

        /// <summary>
        /// Performs Part of Speech Detection 
        /// </summary>
        /// <param name="ugc">User generated text data</param>
        /// <param name="language">Language of the text</param>
        /// <param name="responseListener">Request response listener (via callback)</param>
        /// <returns></returns>
        public async Task<string> RequestPartOfSpeechDetection(
            NLPUserGeneratedContent ugc,
            Language language,
            IResponseListener<PartOfSpeechTags> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, posTagProcessingSuffix + 
                (language == Language.English ? "lang=en" : "lang=zn"))).Id;

            RequestAsyncAndWaitForResponseInCallback(requestID, responseListener);

            return requestID;
        }

        /// <summary>
        /// Performs Sentiment Analaysis
        /// </summary>
        /// <param name="ugc">User generated text data</param>
        /// <param name="language">Language of the text</param>
        /// <param name="responseListener">Request response listener (via callback)</param>
        public async Task<string> RequestSentimentAnalysis(
            NLPUserGeneratedContent ugc,
            Language language,
            IResponseListener<Sentiments> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, sentimentAnalysisSuffix +
                (language == Language.English ? "lang=en" : "lang=zn"))).Id;

            RequestAsyncAndWaitForResponseInCallback(requestID, responseListener);

            return requestID;
        }

        /// <summary>
        /// Performs Named Entity Recognition
        /// </summary>
        /// <param name="ugc">User generated text data</param>
        /// <param name="language">Language of the text</param>
        /// <param name="responseListener">Request response listener (via callback)</param>
        public async Task<string> RequestNERRecognition(
           NLPUserGeneratedContent ugc,
           Language language,
           IResponseListener<NamedEntities> responseListener)
        {
            var requestID = (await obtainRequestID(ugc, nerProcessingSuffix +
                (language == Language.English ? "lang=en" : "lang=zn"))).Id;

            RequestAsyncAndWaitForResponseInCallback(requestID, responseListener);

            return requestID;
        }
    }
}
