using System;
using System.Text.RegularExpressions;
using bBridgeAPISDK.Authorization;
using bBridgeAPISDK.Authorization.Interfaces;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestAuthorization
    {
        [Fact]
        public void TestCanReceiveAuthorizationTokenForUserNameAndPassword()
        {
            var baseUri = new Uri(Resources.bBridgeAPIBaseURI);

            IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
                Resources.bBridgeAPIUserName,
                Resources.bBridgeAPIPassword,
                baseUri);

            Assert.True(Regex.IsMatch(userPasswordAuthorizer.Token, Resources.JWTTokenRegex));
            Assert.Equal(userPasswordAuthorizer.Token.Length, 186);
        }
    }
}
