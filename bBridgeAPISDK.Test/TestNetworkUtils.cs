using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.Common.Authorization.Structs;
using bBridgeAPISDK.Common.Interfaces;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestNetworkUtils
    {
        private readonly IAsyncHttpRequester httpRequester = new HttpRequester(
            TestResources.bBridgeAPIBaseURI);

        [Fact]
        public async Task TestCanUnauthorizedCallApiAndReceiveAuthorizationToken()
        {
            var token = await httpRequester.RequestAsync<AuthorizationToken>("auth",
                new Credentials
                {
                    User = TestResources.bBridgeAPIUserName,
                    Password = TestResources.bBridgeAPIPassword
                });

            Assert.True(Regex.IsMatch(token.Token, TestResources.JWTTokenRegex));
            Assert.Equal(token.Token.Length, 186);
        }
    }
}
