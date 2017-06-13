using System.Text.RegularExpressions;
using Xunit;

namespace bBridgeAPISDK.Test
{
    public class TestAuthorization: APIAuthorizedTest
    {
        [Fact]
        public void TestCanReceiveAuthorizationTokenForUserNameAndPassword()
        {
            Assert.True(Regex.IsMatch(userPasswordAuthorizer.Token, bBridgeAPISDKNET.Test.TestResources.JWTTokenRegex));
        }
    }
}
