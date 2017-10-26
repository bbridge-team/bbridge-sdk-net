using System;
using System.IO;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;
#if !NETCORE
using bBridgeAPISDKNET.Test;
#endif

namespace bBridgeAPISDK.Test
{
    public abstract class APIAuthorizedTest
    {
        protected IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
                string.IsNullOrEmpty(TestResources.bBridgeAPIUserName) ?
                    Environment.GetEnvironmentVariable("BBRIDGE_API_USER_NAME") :
                    TestResources.bBridgeAPIUserName,
                string.IsNullOrEmpty(TestResources.bBridgeAPIUserName) ?
                    Environment.GetEnvironmentVariable("BBRIDGE_API_PASSWORD") :
                    TestResources.bBridgeAPIPassword,
                Path.Combine(TestResources.bBridgeAPIBaseURI,
                TestResources.bBridgeAPIAuthUrlSuffix));
    }
}
