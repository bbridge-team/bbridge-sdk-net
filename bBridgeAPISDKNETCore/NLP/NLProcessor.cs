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
    public class NLProcessor : APIRequester
    {
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
        /// <returns></returns>
        public async Task<PartOfSpeechTags> DetectPartsOfSpeech(
            NLPUserGeneratedContent ugc,
            Language language)
        {
            return await ReceiveResponseAsync<PartOfSpeechTags>(
                (await RequestAsync(ugc, bBridgeAPIURLSuffixes.PosTagProcessingSuffix +
                    (language == Language.English ? "lang=en" : "lang=zn"))).Id);
        }

        /// <summary>
        /// Performs Sentiment Analaysis
        /// </summary>
        /// <param name="ugc">User generated text data</param>
        /// <param name="language">Language of the text</param>
        public async Task<Sentiments> DetectSentiment(
            NLPUserGeneratedContent ugc,
            Language language)
        {
            return await ReceiveResponseAsync<Sentiments>(
                (await RequestAsync(ugc, bBridgeAPIURLSuffixes.SentimentAnalysisSuffix +
                    (language == Language.English ? "lang=en" : "lang=zn"))).Id);
        }

        /// <summary>
        /// Performs Named Entity Recognition
        /// </summary>
        /// <param name="ugc">User generated text data</param>
        /// <param name="language">Language of the text</param>
        public async Task<NamedEntities> RecognizeNamedEntities(
           NLPUserGeneratedContent ugc,
           Language language)
        {
            return await ReceiveResponseAsync<NamedEntities>(
                (await RequestAsync(ugc, bBridgeAPIURLSuffixes.NerProcessingSuffix +
                    (language == Language.English ? "lang=en" : "lang=zn"))).Id);
        }
    }
}
