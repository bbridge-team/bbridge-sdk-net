using System;
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
                TestResources.bBridgeAPIUserName,
                TestResources.bBridgeAPIPassword,
                TestResources.bBridgeAPIBaseURI);

            Assert.Equal(userPasswordAuthorizer.Token.Length, 186);
            Assert.True(Regex.IsMatch(userPasswordAuthorizer.Token, TestResources.JWTTokenRegex));
        }
    }
}
