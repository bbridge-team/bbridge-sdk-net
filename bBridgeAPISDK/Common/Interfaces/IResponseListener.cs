namespace bBridgeAPISDK.Interfaces
{
    public interface IResponseListener<T>
    {
        void ResponseReceived(string requestId, T respond);
    }
}
