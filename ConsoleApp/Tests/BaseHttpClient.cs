﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

 namespace ConsoleApp.Tests
{
    public abstract class BaseHttpClient 
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        protected BaseHttpClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
        }

        public async Task<T> GetAsync<T>(Uri requestUri)
        {
            using var requestMessage = CreateRequestMessage(HttpMethod.Get, requestUri);
            using var responseMessage = await SendAsync(requestMessage);

            return await DeserializeAsync<T>(responseMessage.Content, _jsonSerializerOptions);
        }
        
        public async Task<T> PostAsync<TRequest, T>(Uri requestUri, TRequest requestBody)
        {
            await using var requestStream = await SerializeAsync(requestBody, _jsonSerializerOptions);
            using var streamContent = CreateStreamContent(requestStream);
            using var requestMessage = CreateRequestMessage(HttpMethod.Post, requestUri, streamContent);
            using var responseMessage = await SendAsync(requestMessage);

            return await DeserializeAsync<T>(responseMessage.Content, _jsonSerializerOptions);
        }

        public async Task PutAsync<TRequest>(Uri requestUri, TRequest requestBody)
        {
            await using var requestStream = await SerializeAsync(requestBody, _jsonSerializerOptions);
            using var streamContent = CreateStreamContent(requestStream);
            using var requestMessage = CreateRequestMessage(HttpMethod.Put, requestUri, streamContent);
            using var responseMessage = await SendAsync(requestMessage);
        }

        public async Task<T> DeleteAsync<T>(Uri requestUri)
        {
            using var requestMessage = CreateRequestMessage(HttpMethod.Delete, requestUri);
            using var responseMessage = await SendAsync(requestMessage);
            await using var contentStream = await responseMessage.Content.ReadAsStreamAsync();

            return await DeserializeAsync<T>(responseMessage.Content, _jsonSerializerOptions);
        }

        private static async Task<Stream> SerializeAsync<T>(T requestBody, JsonSerializerOptions jsonSerializerOptions)
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, requestBody, jsonSerializerOptions);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private static async Task<T> DeserializeAsync<T>(HttpContent httpContent, JsonSerializerOptions jsonSerializerOptions)
        {
            await using var contentStream = await httpContent.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream, jsonSerializerOptions);
        }

        private static HttpContent CreateStreamContent(Stream stream)
        {
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return streamContent;
        }

        private static HttpRequestMessage CreateRequestMessage(HttpMethod method, Uri requestUri, HttpContent streamContent)
        {
            var requestMessage = new HttpRequestMessage(method, requestUri) {Content = streamContent};
            return requestMessage;
        }

        private static HttpRequestMessage CreateRequestMessage(HttpMethod method, Uri requestUri)
        {
            var requestMessage = new HttpRequestMessage(method, requestUri);
            return requestMessage;
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage;
        }
    }
}
