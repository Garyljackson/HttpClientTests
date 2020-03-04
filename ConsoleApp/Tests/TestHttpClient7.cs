using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

 namespace ConsoleApp.Tests
{
    public class TestHttpClient7 
    {
        private readonly HttpClient _httpClient;

        public TestHttpClient7(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // This needs to be the root of the url, I tested it by including /api/books/ and that didn't work
            // Could also be configured using services.AddHttpClient
            _httpClient.BaseAddress = new Uri("https://localhost:44342/"); 
        }

        public async Task<T> GetAsync<T>(Uri requestUri)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            using var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            await using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream, DefaultJsonSerializerOptions.Options);
        }
        
        public async Task<TResponse> PostAsync<TRequest, TResponse>(Uri requestUri, TRequest requestBody)
        {
            await using var requestStream = await SerializeAsync(requestBody);

            using var streamContent = GetContent(requestStream);

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri) {Content = streamContent};

            using var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            responseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(contentStream, DefaultJsonSerializerOptions.Options);
        }

        public async Task PutAsync<TRequest>(Uri requestUri, TRequest requestBody)
        {
            await using var requestStream = await SerializeAsync(requestBody);

            using var streamContent = GetContent(requestStream);

            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri) {Content = streamContent};

            using var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<T> DeleteAsync<T>(Uri requestUri)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            using var responseMessage = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            await using var contentStream = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream, DefaultJsonSerializerOptions.Options);
        }

        private async Task<Stream> SerializeAsync<T>(T requestBody)
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, requestBody, DefaultJsonSerializerOptions.Options);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private StreamContent GetContent(Stream stream)
        {
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return streamContent;
        }
    }
}
