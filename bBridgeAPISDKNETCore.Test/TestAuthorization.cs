using Xunit;

#if NETCORE
using bBridgeAPISDKNETCore.Test;
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
