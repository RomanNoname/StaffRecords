namespace StaffRecords.WEB.Requests
{
    public interface IHttpApiRequests
    {
        Task<HttpResponseMessage> SendGetAsyncRequest(string requestUri);

        Task<HttpResponseMessage> SendPostAsyncRequest<TValue>(string requestUri, TValue value);

        Task<HttpResponseMessage> SendPutAsyncRequest<TValue>(string requestUri, TValue value);

        Task<HttpResponseMessage> SendDeleteAsyncRequest(string requestUri);
    }
}
