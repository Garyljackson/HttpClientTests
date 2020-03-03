using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.Tests
{
    public class TestHttpClient6 
    {
        private readonly HttpClient _httpClient;

        public TestHttpClient6(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // This needs to be the root of the url, I tested it by including /api/books/ and that didn't work
            // Could also be configured using services.AddHttpClient
            _httpClient.BaseAddress = new Uri("https://localhost:44342/"); 
        }

        public async Task<T> GetAsync<T>(Uri requestUri)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    await using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(contentStream, DefaultJsonSerializerOptions.Options);
                    }
                }
            }
        }
        
        public async Task<T> PostAsync<T>(Uri requestUri, object requestBody)
        {
            using (var requestStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(requestStream, requestBody, DefaultJsonSerializerOptions.Options);
                requestStream.Seek(0, SeekOrigin.Begin);

                using (var streamContent = new StreamContent(requestStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
                    {
                        requestMessage.Content = streamContent;

                        using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                        {
                            await using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                            {
                                return await JsonSerializer.DeserializeAsync<T>(contentStream, DefaultJsonSerializerOptions.Options);
                            }
                        }
                    }
                }
            }
        }

        public async Task PutAsync(Uri requestUri, object requestBody)
        {
            using (var requestStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(requestStream, requestBody, DefaultJsonSerializerOptions.Options);
                requestStream.Seek(0, SeekOrigin.Begin);

                using (var streamContent = new StreamContent(requestStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
                    {
                        requestMessage.Content = streamContent;

                        await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
                    }
                }
            }
        }

        public async Task<T> DeleteAsync<T>(Uri requestUri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    await using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(contentStream, DefaultJsonSerializerOptions.Options);
                    }
                }
            }
        }
    }
}
