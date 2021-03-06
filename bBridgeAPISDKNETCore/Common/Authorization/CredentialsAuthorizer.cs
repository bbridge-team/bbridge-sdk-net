﻿using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Authorization.Structs;


namespace bBridgeAPISDK.Common.Authorization
{
    public class LazyCredentialsAuthorizer: IAuthorizer
    {
        #region Fields

        private string token;
	    readonly string authUrl;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates Authorizer that requests token by providing username and password
        /// </summary> 
        /// <param name="username">Name of bBridge API user</param>
        /// <param name="password">Password of bBridge API user</param>
        /// <param name="authUrl">bBridge API authentication request URL</param>
        public LazyCredentialsAuthorizer(string username, string password, string authUrl)
        {
            UserName = username;
            Password = password;
			this.authUrl = authUrl;
        }

        #endregion

        #region Properties

        /// <summary>
        /// bBridge API password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// bBridge API user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// API authorization Token
        /// </summary>
        public string Token
        {
            get
            {
                if (string.IsNullOrEmpty(token))
                {
					using(var httpClient = new HttpClient())
					{
                        httpClient.Timeout = TimeSpan.FromMinutes(1);

						token = JsonConvert.DeserializeObject<AuthorizationToken>(
							httpClient.PostAsync(authUrl,
								new StringContent(JsonConvert.SerializeObject(
									new Credentials { User = UserName, Password = Password}), 
						        Encoding.UTF8, "application/json")).
							Result.Content.ReadAsStringAsync().Result).Token;
					}
                }

                return token;
            }
        }

        #endregion
    }
}
