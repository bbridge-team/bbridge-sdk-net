using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Enums;
using bBridgeAPISDK.NLP;
using bBridgeAPISDK.NLP.Structs;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestNLP: APIAuthorizedTest
    {
		private readonly NLProcessor nlpProcessor;

        public TestNLP()
        {
            //Wait 60 times 1 seconds each
            nlpProcessor = new NLProcessor(
                new AuthorizedHttpRequester(bBridgeAPISDKNET.Test.TestResources.bBridgeAPIBaseURI, TimeSpan.FromMinutes(10),  userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };
        }

        [Fact]
        public async Task TestCanRequestPOSDetectionAndReceiveResults()
        {
            var result = await nlpProcessor.DetectPartsOfSpeech(
                new NLPUserGeneratedContent(new List<string> { "Putin is Trump's friend", "The weather is good :)" }),
                Language.English);

            foreach (var tagList in result.PartOfSpeechTagLists)
            {
                foreach (var tag in tagList)
                {
                    Assert.False(string.IsNullOrEmpty(tag.Text));
                    Assert.False(string.IsNullOrEmpty(tag.Type));
                }
            }
        }


        [Fact]
        public async Task TestCanRequestSentimentDetectionAndReceiveResults()
        {
            var result = await nlpProcessor.DetectSentiment(
                new NLPUserGeneratedContent(new List<string> { "Putin is Trump's friend", "The weather is good :)" }),
                Language.English);
            
            foreach (var sentiment in result.SentimentsList)
            {
                Assert.InRange(sentiment, 0, 1);
            }
        }

        [Fact]
        public async Task TestCanRequestNERDetectionAndReceiveResults()
        {
            var result = await nlpProcessor.RecognizeNamedEntities(
                new NLPUserGeneratedContent(new List<string>
                    { "Putin is Trump's friend", "The weather is good :)" }),
                Language.English);

            foreach (var nerList in result.NamedEntityLists)
            {
                foreach (var ner in nerList)
                {
                    Assert.InRange(ner.Count, 1, int.MaxValue);
                    Assert.False(string.IsNullOrEmpty(ner.Text));
                    Assert.False(string.IsNullOrEmpty(ner.Type));
                }
            }
        }

    }
}
