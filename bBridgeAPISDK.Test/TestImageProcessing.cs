﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.ImageProcessing;
using bBridgeAPISDK.ImageProcessing.Structs;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestImageProcessing: APIAuthorizedTest
    {
		readonly ImageProcessor imageProcessor;

		public TestImageProcessing()
        {
            //Wait 60 times 1 seconds each
            imageProcessor = new ImageProcessor(new AuthorizedHttpRequester(TestResources.bBridgeAPIBaseURI, userPasswordAuthorizer))
            {
                ResponseWaitNumAttempts = 60,
                ResponseWaitTime = TimeSpan.FromSeconds(1)
            };
        }

        [Fact]
        public async Task TestCanRequestObjectDetectionAndReceiveResults()
        {
            var result = await imageProcessor.DetectImageObjects(
                new ObjectDetectionData("https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg", 0.5));

            foreach (var obj in result.Objects)
            {
                Assert.False(string.IsNullOrEmpty(obj.Name));
                Assert.InRange(obj.Score, 0.00001, 1);
            }
        }

        [Fact]
        public async Task TestCanRequestConceptDetectionAndReceiveResults()
        {
            var result = await imageProcessor.DetectImageConcepts(
                new ConceptDetectionData(
                    new List<string>
                        {
                            "https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg",
                            "https://pbs.twimg.com/media/C6jO3UiVoAQYc_8.jpg"
                        }, 5));

            foreach (var distribution in result.ImageConceptDistributions)
            {
                Assert.Equal(distribution.Concepts.Count, 5);

                foreach (var concept in distribution.Concepts)
                {
                    Assert.False(string.IsNullOrEmpty(concept.Key));
                    Assert.InRange(concept.Value, 0.000001, 1);
                }
            }
        }
    }
}
