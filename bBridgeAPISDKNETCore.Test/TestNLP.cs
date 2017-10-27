using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Enums;
using bBridgeAPISDK.NLP;
using bBridgeAPISDK.NLP.Structs;
using Xunit;

#if NETCORE
using bBridgeAPISDKNETCore.Test;
#endif

namespace bBridgeAPISDK.Test
{
    public class TestNLP: APIAuthorizedTest
    {
		private readonly NLProcessor nlpProcessor;

        public TestNLP()
        {
            //Wait 60 times 1 seconds each
            nlpProcessor = new NLProcessor(
                new AuthorizedHttpRequester(TestResources.bBridgeAPIBaseURI, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };
        }

        [Fact]
        public async Task TestCanRequestEnglishPOSDetectionAndReceiveResults()
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

        [Fact(Skip = "Not implemented yet")]
        public async Task TestCanRequestChinesePOSDetectionAndReceiveResults()
        {
            var result = await nlpProcessor.DetectPartsOfSpeech(
                new NLPUserGeneratedContent(new List<string> { "什么美丽的画面!", "今天是一个非常美好的一天 :)" }),
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
        public async Task TestCanRequestEnglishSentimentDetectionAndReceiveResults()
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
        public async Task TestCanRequestChineseSentimentDetectionAndReceiveResults()
        {
            var result = await nlpProcessor.DetectSentiment(
                new NLPUserGeneratedContent(new List<string> { "什么美丽的画面!", "今天是一个非常美好的一天 :)" }),
                Language.English);

            foreach (var sentiment in result.SentimentsList)
            {
                Assert.InRange(sentiment, 0, 1);
            }
        }

        [Fact]
        public async Task TestCanRequestEnglishNERDetectionAndReceiveResults()
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

        [Fact(Skip = "Not implemented yet")]
        public async Task TestCanRequestChineseNERDetectionAndReceiveResults()
        {
            var result = await nlpProcessor.RecognizeNamedEntities(
                new NLPUserGeneratedContent(new List<string>
                    { "什么美丽的画面!", "今天是一个非常美好的一天 :)" }),
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
