using bBridgeAPISDK.Common.Authorization.Interfaces;

namespace bBridgeAPISDK.Common.Authorization
{
    public class StoredTokenAuthorizer: IAuthorizer
    {
        #region Constructors

        /// <summary>
        /// Creates Authorizer based on pre-requested token
        /// </summary>
        /// <param name="token">API authorization Token</param>
        public StoredTokenAuthorizer(string token)
        {
            Token = token;
        }

        #endregion

        #region Properties

        /// <summary>
        /// API authorization Token
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public string Token { get; private set; }

        #endregion
    }
}
