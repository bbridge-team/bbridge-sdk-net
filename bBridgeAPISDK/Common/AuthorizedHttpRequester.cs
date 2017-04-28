using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Interfaces;
using Newtonsoft.Json;

namespace bBridgeAPISDK.Common
{
    public class AuthorizedHttpRequester : IAsyncHttpRequester
    {
        #region Fields

        readonly HttpClient httpClient = new HttpClient();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of basic authorized http requester
        /// </summary>
        /// <param name="baseUrl">Base API URL</param>
        /// <param name="authorizer">Authorizer of a user</param>
        public AuthorizedHttpRequester(string baseUrl, IAuthorizer authorizer):
            this(baseUrl, TimeSpan.MaxValue, authorizer)
        {          
        }

        /// <summary>
        /// Constructor of basic authorized http requester
        /// </summary>
        /// <param name="baseUrl">Base API URL</param>
        /// <param name="httpTimeout">HTTP Timeout</param>
        /// <param name="authorizer">Authorizer of a user</param>
        public AuthorizedHttpRequester(string baseUrl, TimeSpan httpTimeout, IAuthorizer authorizer)
        {
            if (authorizer == null)
            {
                throw new ArgumentException("authorizer can not be null");
            }

            httpClient.Timeout = httpTimeout;
            httpClient.BaseAddress = new Uri(baseUrl);

            //This supposed to be changed when we follow the standart
            //authorization protocol with auth method specified
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", authorizer?.Token);
            httpClient.DefaultRequestHeaders.Add("Authorization", authorizer.Token);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Makes async http request in POST if data specified and GET if data is not specified
        /// </summary>
        /// <typeparam name="T">Result deserialization type</typeparam>
        /// <param name="urlSuffix">Suffix of the aprticular URL request</param>
        /// <param name="data">Data to be sent in POST request body</param>
        /// <returns>Request result deserialized as T</returns>
        public async Task<T> RequestAsync<T>(string urlSuffix, object data = null) where T : class
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, urlSuffix))
            {
                if (data != null)
                {
                    request.Method = HttpMethod.Post;
                    request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                }

                return await httpClient.SendAsync(request).ContinueWith(responseTask =>
                    {
                        if (responseTask.Result.StatusCode == HttpStatusCode.OK ||
                            responseTask.Result.StatusCode == HttpStatusCode.Accepted)
                            return responseTask.Result.Content.ReadAsStringAsync().
                                ContinueWith(jsonTask => JsonConvert.DeserializeObject<T>(jsonTask.Result));

                        if (responseTask.Result.StatusCode == HttpStatusCode.NoContent) return null;

                        throw new HttpRequestException(responseTask.Result.StatusCode.ToString());
                    }
                ).Unwrap();
            }
        }

        #endregion
    }
}
