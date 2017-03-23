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
    public class HttpRequester : IAsyncHttpRequester
    {
        private readonly IAuthorizer authorizer;
        /// <summary>
        /// Constructor of basic http requester
        /// </summary>
        /// <param name="baseAdress">Base API adress</param>
        /// <param name="authorizer">Authorizer of a user (if needed)</param>
        public HttpRequester(string baseAdress, IAuthorizer authorizer = null)
        {
            this.authorizer = authorizer;
            BaseAdress = baseAdress;
        }
        /// <summary>
        /// Based API URI
        /// </summary>
        public string BaseAdress { get; }

        /// <summary>
        /// Makes async http request in POST if data specified and GET if data is not specified
        /// </summary>
        /// <typeparam name="T">Result deserialization type</typeparam>
        /// <param name="urlSuffix">Suffix of the aprticular URL request</param>
        /// <param name="data">Data to be sent in POST request body</param>
        /// <returns>Request result deserialized as T</returns>
        public async Task<T> RequestAsync<T>(string urlSuffix, object data = null) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAdress);

                if (authorizer != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", authorizer.Token);
                    //TODO: This supposed to be changed when we follow the standart
                    //authorization protocol with auth method specified
                    //client.DefaultRequestHeaders.Authorization =
                    //    new AuthenticationHeaderValue("Bearer", authorizer?.Token);
                }

                HttpRequestMessage request;

                if (data != null)
                {
                    request = new HttpRequestMessage(HttpMethod.Post, urlSuffix)
                    {
                        Content =
                            new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                    };
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Get, urlSuffix);
                }

                return await client.SendAsync(request).ContinueWith(responseTask =>
                    {
                        if (responseTask.Result.IsSuccessStatusCode)
                            return responseTask.Result.Content.ReadAsStringAsync().
                                ContinueWith(jsonTask => JsonConvert.DeserializeObject<T>(jsonTask.Result));

                        if (responseTask.Result.StatusCode == HttpStatusCode.NoContent) return null;

                        throw new HttpRequestException(responseTask.Result.StatusCode.ToString());
                    }
                ).Unwrap();
            }
        }
    }
}
