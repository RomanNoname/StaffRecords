﻿using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace StaffRecords.Frontend.Shared.Requests
{
    public class HttpRequests : IHttpApiRequests
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navManager;

        public HttpRequests(IHttpClientFactory factory, NavigationManager navManager, string clientName)
        {
            _httpClient = factory.CreateClient(clientName);
            _navManager = navManager;
        }

        public async Task<HttpResponseMessage> SendGetAsyncRequest(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);

            CheckResponseStatusCode(response);

            return response;
        }

        public async Task<HttpResponseMessage> SendPostAsyncRequest<TValue>(string requestUri, TValue value)
        {
            var response = await _httpClient.PostAsJsonAsync(requestUri, value);

            CheckResponseStatusCode(response);

            return response;
        }

        public async Task<HttpResponseMessage> SendPutAsyncRequest<TValue>(string requestUri, TValue value)
        {
            var response = await _httpClient.PutAsJsonAsync(requestUri, value);

            CheckResponseStatusCode(response);

            return response;
        }

        public async Task<HttpResponseMessage> SendDeleteAsyncRequest(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);

            CheckResponseStatusCode(response);

            return response;
        }

        private void CheckResponseStatusCode(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = response.StatusCode;
                switch (statusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/login");
                        break;
                    default:
                        _navManager.NavigateTo("/error");
                        break;
                }
            }
        }
    }
}
