namespace bBridgeAPISDK.Common.Authorization.Interfaces
{
	public interface IAuthorizer
    {
        /// <summary>
        /// API Authorization Token 
        /// </summary>
        string Token { get; }
    }
}
