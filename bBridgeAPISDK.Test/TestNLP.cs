﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Enums;
using bBridgeAPISDK.Common.Interfaces;
using bBridgeAPISDK.NLP;
using bBridgeAPISDK.NLP.Structs;
using Moq;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestNLP
    {
		readonly IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
			TestResources.bBridgeAPIUserName,
			TestResources.bBridgeAPIPassword,
			TestResources.bBridgeAPIBaseURI);

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
            var responseReceived = new AutoResetEvent(false);
            var mockResponseListener = new Mock<IResponseListener<PartOfSpeechTags>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsNotNull<string>(),
                    It.IsNotNull<PartOfSpeechTags>())).
                    Callback<string, PartOfSpeechTags>((id, tags) =>
                    {
                        //Signaling that the result was called back
                        responseReceived.Set();

                        //Check if all attributes are not empty and not null
                        Assert.False(string.IsNullOrEmpty(id));
                        Assert.Equal(tags.PartOfSpeechTagLists.Count, 2);

                        foreach (var tagList in tags.PartOfSpeechTagLists)
                        {
                            foreach (var tag in tagList)
                            {
                                Assert.False(string.IsNullOrEmpty(tag.Text));
                                Assert.False(string.IsNullOrEmpty(tag.Type));
                            }
                        }
                    }).Verifiable();

            await nlpProcessor.RequestPartOfSpeechDetection(
                new NLPUserGeneratedContent(new List<string> { "Putin is Trump's friend", "The weather is good :)" }),
                Language.English,
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }


        [Fact]
        public async Task TestCanRequestSentimentDetectionAndReceiveResultsInCallback()
        {
            var responseReceived = new AutoResetEvent(false);
            var mockResponseListener = new Mock<IResponseListener<Sentiments>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsNotNull<string>(),
                    It.IsNotNull<Sentiments>())).
                    Callback<string, Sentiments>((id, sentiments) =>
                    {
                        //Signaling that the result was called back
                        responseReceived.Set();

                        //Check if all attributes are not empty and not null
                        Assert.False(string.IsNullOrEmpty(id));
                        Assert.Equal(sentiments.SentimentsList.Count, 2);

                        foreach (var sentiment in sentiments.SentimentsList)
                        {
                            Assert.InRange(sentiment, 0, 1);
                        }
                    }).Verifiable();

            await nlpProcessor.RequestSentimentAnalysis(
                new NLPUserGeneratedContent(new List<string> { "Putin is Trump's friend", "The weather is good :)" }),
                Language.English,
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }

        [Fact]
        public async Task TestCanRequestNERDetectionAndReceiveResultsInCallback()
        {
            var responseReceived = new AutoResetEvent(false);
            var mockResponseListener = new Mock<IResponseListener<NamedEntities>>();
            mockResponseListener.Setup(
                m => m.ResponseReceived(
                    It.IsNotNull<string>(),
                    It.IsNotNull<NamedEntities>())).
                    Callback<string, NamedEntities>((id, ners) =>
                    {
                        //Signaling that the result was called back
                        responseReceived.Set();

                        //Check if all attributes are not empty and not null
                        Assert.False(string.IsNullOrEmpty(id));
                        Assert.Equal(ners.NamedEntityLists.Count, 2);

                        foreach (var nerList in ners.NamedEntityLists)
                        {
                            foreach (var ner in nerList)
                            {
                                Assert.InRange(ner.Count, 1, int.MaxValue);
                                Assert.False(string.IsNullOrEmpty(ner.Text));
                                Assert.False(string.IsNullOrEmpty(ner.Type));
                            }
                        }
                    }).Verifiable();

            await nlpProcessor.RequestNERRecognition(
                new NLPUserGeneratedContent(new List<string> { "Putin is Trump's friend", "The weather is good :)" }),
                Language.English,
                mockResponseListener.Object);

            //Waiting for response for 1 minute
            Assert.True(responseReceived.WaitOne(TimeSpan.FromMinutes(1)));
        }

    }
}
