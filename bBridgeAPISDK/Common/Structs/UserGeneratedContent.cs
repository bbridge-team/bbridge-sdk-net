using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserGeneratedContent
    {
        #region Constructors

        /// <summary>
        /// Creates the structure with the specified texsts and image urls 
        /// </summary>
        /// <param name="texsts">User texsts from social media</param>
        /// <param name="imageURLs">URLs of the images from user's profile</param>
        public UserGeneratedContent(IEnumerable<string> texsts, IEnumerable<string> imageURLs)
        {
            if (texsts == null && imageURLs == null)
            {
                throw new ArgumentException("Both arguments can not be null");
            }

            Texsts = new List<string>(texsts);
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