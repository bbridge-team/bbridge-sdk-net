using System;
using System.IO;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;

namespace bBridgeAPISDK.Test
{
    public abstract class APIAuthorizedTest
    {
        protected IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
                string.IsNullOrEmpty(bBridgeAPISDKNET.Test.TestResources.bBridgeAPIUserName) ?
                    Environment.GetEnvironmentVariable("BBRIDGE_API_USER_NAME") :
                    bBridgeAPISDKNET.Test.TestResources.bBridgeAPIUserName,
                string.IsNullOrEmpty(bBridgeAPISDKNET.Test.TestResources.bBridgeAPIUserName) ?
                    Environment.GetEnvironmentVariable("BBRIDGE_API_PASSWORD") :
                    bBridgeAPISDKNET.Test.TestResources.bBridgeAPIPassword,
                Path.Combine(bBridgeAPISDKNET.Test.TestResources.bBridgeAPIBaseURI,
                bBridgeAPISDKNET.Test.TestResources.bBridgeAPIAuthUrlSuffix));
    }
}
