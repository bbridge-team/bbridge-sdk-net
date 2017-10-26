using System.Text.RegularExpressions;
using Xunit;
#if !NETCORE
using bBridgeAPISDKNET.Test;
#endif

namespace bBridgeAPISDK.Test
{
    public class TestAuthorization: APIAuthorizedTest
    {
        [Fact]
        public void TestCanReceiveAuthorizationTokenForUserNameAndPassword()
        {
            Assert.Matches(TestResources.JWTTokenRegex, userPasswordAuthorizer.Token);
        }
    }
}
