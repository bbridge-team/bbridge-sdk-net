using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NLPUserGeneratedContent
    {
        /// <summary>
        /// Creates user generated content data structure for NLP processor
        /// </summary>
        /// <param name="sentences">Sentences to process</param>
        public NLPUserGeneratedContent(IEnumerable<string> sentences)
        {
            if (sentences == null)
            {
                throw new ArgumentException("sentances can not be null");
            }

            Sentences = new List<string>(sentences);
        }

        [JsonProperty(PropertyName = "sentences")]
        public List<string> Sentences { get; set; }
    }
}
