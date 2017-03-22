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
        public HttpRequester(Uri baseAdress, IAuthorizer authorizer = null)
        {
            this.authorizer = authorizer;
            BaseAdress = baseAdress;
        }
        public Uri BaseAdress { get; }

        public async Task<T> RequestAsync<T>(string urlSuffix, object data = null) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAdress;

                if (authorizer != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", authorizer.Token);
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
