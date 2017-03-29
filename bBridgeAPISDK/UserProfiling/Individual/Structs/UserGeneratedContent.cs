using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.UserProfiling.Individual.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserGeneratedContent
    {
        #region Constructors

        /// <summary>
        /// Creates the structure with the specified texsts and image urls 
        /// </summary>
        /// <param name="texts">User texsts from social media</param>
        /// <param name="imageURLs">URLs of the images from user's profile</param>
        public UserGeneratedContent(IEnumerable<string> texts, IEnumerable<string> imageURLs)
        {
			if (texts == null && imageURLs == null)
            {
                throw new ArgumentException("Both arguments can not be null");
            }

			Texsts = new List<string>(texts);
            ImageURLs = imageURLs;
        }

        #endregion

        #region Properties

        [JsonProperty(PropertyName = "text")]
        public IEnumerable<string> Texsts { get; set; }

        [JsonProperty(PropertyName = "image_urls")]
        public IEnumerable<string> ImageURLs { get; set; }

        #endregion
    }
}
