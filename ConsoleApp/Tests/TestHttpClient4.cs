using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public class TestHttpClient4 : ITestHttpClient
    {
        private readonly HttpClient _httpClient;

        public TestHttpClient4(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // This needs to be the root of the url, I tested it by including /api/books/ and that didn't work
            // Could also be configured using services.AddHttpClient
            _httpClient.BaseAddress = new Uri("https://localhost:44342/"); 
        }

        public async Task<IEnumerable<BookResponse>> GetListAsync()
        {
            var requestUri = new Uri("/api/books", UriKind.Relative);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<IEnumerable<BookResponse>>(contentStream, DefaultJsonSerializerOptions.Options);
                    }
                }
            }
        }

        public async Task<BookResponse> GetAsync(Guid id)
        {
            var requestUri = new Uri($"/api/books/{id}", UriKind.Relative);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<BookResponse>(contentStream, DefaultJsonSerializerOptions.Options);
                    }
                }
            }
        }

        public async Task<BookResponse> PostAsync(BookPostRequest request)
        {
            var requestUri = new Uri("/api/books", UriKind.Relative);

            var bookJson = JsonSerializer.Serialize(request);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
            {
                requestMessage.Content = new StringContent(bookJson, Encoding.UTF8, "application/json");

                using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<BookResponse>(contentStream, DefaultJsonSerializerOptions.Options);
                    }
                }
            }
        }

        public async Task PutAsync(BookPutRequest request)
        {
            var requestUri = new Uri($"/api/books/{request.Id}", UriKind.Relative);

            var bookJson = JsonSerializer.Serialize(request);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri))
            {
                requestMessage.Content = new StringContent(bookJson, Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            }
        }

        public async Task<BookResponse> DeleteAsync(Guid id)
        {
            var requestUri = new Uri($"/api/books/{id}", UriKind.Relative);

            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var contentStream = await responseMessage.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<BookResponse>(contentStream, DefaultJsonSerializerOptions.Options);
                    }
                }
            }
        }
    }
}
