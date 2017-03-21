using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserGeneratedContent
    {
        #region Constructors

        /// <summary>
        /// Creates the structure with the specified texsts 
        /// </summary>
        /// <param name="texsts"></param>
        public UserGeneratedContent(IEnumerable<string> texsts) :
            this(texsts, new List<Uri>())
        {
        }

        /// <summary>
        /// Creates the structure with the image URLs 
        /// </summary>
        /// <param name="imageURLs">URLs of the images from user's profile</param>
        public UserGeneratedContent(IEnumerable<Uri> imageURLs) :
            this(new List<string>(), imageURLs)
        {
        }

        /// <summary>
        /// Creates the structure with the specified texsts and image urls 
        /// </summary>
        /// <param name="texsts">User texsts from social media</param>
        /// <param name="imageURLs">URLs of the images from user's profile</param>
        public UserGeneratedContent(IEnumerable<string> texsts, IEnumerable<Uri> imageURLs)
        {
            Texsts = new List<string>(texsts);
            ImageURLs = imageURLs.Select(url => url.ToString()).ToList();
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