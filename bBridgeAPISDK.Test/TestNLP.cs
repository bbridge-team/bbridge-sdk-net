using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Enums;
using bBridgeAPISDK.NLP;
using bBridgeAPISDK.NLP.Structs;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestNLP
    {
		readonly IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
			string.IsNullOrEmpty(TestResources.bBridgeAPIUserName) ?
					Environment.GetEnvironmentVariable("bBridgeAPIUserName") :
					TestResources.bBridgeAPIUserName,
			string.IsNullOrEmpty(TestResources.bBridgeAPIUserName) ?
					Environment.GetEnvironmentVariable("bBridgeAPIPassword") :
					TestResources.bBridgeAPIPassword,
            Path.Combine(TestResources.bBridgeAPIBaseURI,
                TestResources.bBridgeAPIAuthUrlSuffix));

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
        public async Task TestCanRequestPOSDetectionAndReceiveResultsInCallback()
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
        public async Task TestCanRequestSentimentDetectionAndReceiveResultsInCallback()
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
        public async Task TestCanRequestNERDetectionAndReceiveResultsInCallback()
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
