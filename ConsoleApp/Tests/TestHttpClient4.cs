using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public class TestHttpClient4
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
                using (var responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<BookResponse>>(responseJson, DefaultJsonSerializerOptions.Options);
                }
            }
        }

        public async Task<BookResponse> GetAsync(Guid id)
        {
            var requestUri = new Uri($"/api/books/{id}", UriKind.Relative);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
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

                using (var responseMessage = await _httpClient.SendAsync(requestMessage))
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
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
                await _httpClient.SendAsync(requestMessage);
            }
        }

        public async Task<BookResponse> DeleteAsync(Guid id)
        {
            var requestUri = new Uri($"/api/books/{id}", UriKind.Relative);

            using (var request = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                using (var responseMessage = await _httpClient.SendAsync(request))
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
                }
            }
        }
    }
}
