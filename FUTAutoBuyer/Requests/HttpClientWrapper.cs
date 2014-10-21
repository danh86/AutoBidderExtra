﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FUTAutoBuyer.Requests
{
    internal class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientHandler MessageHandler { get; set; }

        public HttpClientWrapper()
        {
            MessageHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };
            _httpClient = new HttpClient(MessageHandler);
        }

        public void AddRequestHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
        }

        public void RemoveRequestHeader(string name)
        {
            _httpClient.DefaultRequestHeaders.Remove(name);
        }

        public void SetExpectContinueHeaderToFalse()
        {
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        public void AddConnectionKeepAliveHeader()
        {
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        public void SetReferrerUri(string value)
        {
            _httpClient.DefaultRequestHeaders.Referrer = new Uri(value);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _httpClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent httpContent)
        {
            return _httpClient.PostAsync(requestUri, httpContent);
        }

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent httpContent)
        {
            return _httpClient.PostAsync(requestUri, httpContent);
        }

        public Task<byte[]> GetByteArrayAsync(string requestUri)
        {
            return _httpClient.GetByteArrayAsync(requestUri);
        }
    }
}
