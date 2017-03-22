using System;
using System.IO;
using System.Text.RegularExpressions;
using bBridgeAPISDK.Common.Authorization;
using bBridgeAPISDK.Common.Authorization.Interfaces;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestAuthorization
    {
        [Fact]
        public void TestCanReceiveAuthorizationTokenForUserNameAndPassword()
        {
            IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
				string.IsNullOrEmpty(TestResources.bBridgeAPIUserName) ?
					Environment.GetEnvironmentVariable("BBRIDGE_API_USER_NAME") : 
					TestResources.bBridgeAPIUserName,
				string.IsNullOrEmpty(TestResources.bBridgeAPIUserName) ?
					Environment.GetEnvironmentVariable("BBRIDGE_API_PASSWORD") :
					TestResources.bBridgeAPIPassword,
                Path.Combine(TestResources.bBridgeAPIBaseURI,
                TestResources.bBridgeAPIAuthUrlSuffix));

            Assert.Equal(userPasswordAuthorizer.Token.Length, 186);
            Assert.True(Regex.IsMatch(userPasswordAuthorizer.Token, TestResources.JWTTokenRegex));
        }
    }
}
