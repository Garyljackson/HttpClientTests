using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public class Gen5HttpClient : ITestHttpClient
    {
        private readonly HttpClient _httpClient;

        public Gen5HttpClient(HttpClient httpClient)
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

            using (var requestStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(requestStream, request, DefaultJsonSerializerOptions.Options);
                requestStream.Seek(0, SeekOrigin.Begin);

                using (var streamContent = new StreamContent(requestStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
                    {
                        requestMessage.Content = streamContent;

                        using (var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
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

        public async Task PutAsync(BookPutRequest request)
        {
            var requestUri = new Uri($"/api/books/{request.Id}", UriKind.Relative);

            using (var requestStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(requestStream, request, DefaultJsonSerializerOptions.Options);
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
