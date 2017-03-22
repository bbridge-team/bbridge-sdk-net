using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Enums;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.NLP.Structs;

namespace bBridgeAPISDK.NLP
{
    public class NLProcessor : APIFeature
    {
        private const string posTagProcessingSuffix = "nlp/pos?";
        private const string sentimentAnalysisSuffix = "nlp/sentiment?";
        private const string nerProcessingSuffix = "nlp/ner?";

        public NLProcessor(IAsyncHttpRequester requester) :
            base(requester)
        { }

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
