using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using bBridgeAPISDK.Structs;
using bBridgeAPISDK.Utils;
using bBridgeAPISDK.Utils.Interfaces;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestNetworkUtils
    {
        private readonly IAsyncHttpRequester httpRequester = new HttpRequester(
            new Uri(Resources.bBridgeAPIBaseURI));

        [Fact]
        public async Task TestCanUnauthorizedCallApiAndReceiveAuthorizationToken()
        {
            var token = await httpRequester.RequestAsync<AuthorizationToken>("auth",
                new Credentials
                {
                    User = Resources.bBridgeAPIUserName,
                    Password = Resources.bBridgeAPIPassword
                });

            Assert.True(Regex.IsMatch(token.Token, Resources.JWTTokenRegex));
            Assert.Equal(token.Token.Length, 186);
        }
    }
}
