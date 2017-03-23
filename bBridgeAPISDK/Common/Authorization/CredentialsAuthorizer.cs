using System;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using bBridgeAPISDK.Common.Authorization.Structs;
using bBridgeAPISDK.Common.Interfaces;

namespace bBridgeAPISDK.Common.Authorization
{
    public class LazyCredentialsAuthorizer: IAuthorizer
    {
        #region Fields

        private readonly IAsyncHttpRequester requester;
        private string token;
        private string urlSuffix = "auth";

        #endregion

        #region Constructors

        /// <summary>
        /// Creates Authorizer that requests token by providing username and password
        /// </summary> 
        /// <param name="username">Name of bBridge API user</param>
        /// <param name="password">Password of bBridge API user</param>
        public LazyCredentialsAuthorizer(string username, string password, string authBaseUri)
        {
            UserName = username;
            Password = password;

            requester = new HttpRequester(authBaseUri);
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
                    token = requester.RequestAsync<AuthorizationToken>(urlSuffix,
                        new Credentials
                        {
                            User = UserName,
                            Password = Password
                        }).Result.Token;
                }

                return token;
            }
        }

        #endregion
    }
}
