using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public class TestHttpClient2
    {
        private readonly HttpClient _httpClient;

        public TestHttpClient2(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookResponse>> GetListAsync()
        {
            var requestUri = new Uri("https://localhost:44342/api/books");

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<BookResponse>>(responseJson, DefaultJsonSerializerOptions.Options);
            }
        }

        public async Task<BookResponse> GetAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
            }
        }

        public async Task<BookResponse> PostAsync(BookPostRequest request)
        {
            var requestUri = new Uri("https://localhost:44342/api/books");

            var bookJson = JsonSerializer.Serialize(request);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
            {
                requestMessage.Content = new StringContent(bookJson, Encoding.UTF8, "application/json");

                var responseMessage = await _httpClient.SendAsync(requestMessage);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
            }
        }

        public async Task PutAsync(BookPutRequest request)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{request.Id}");

            var bookJson = JsonSerializer.Serialize(request);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri))
            {
                requestMessage.Content = new StringContent(bookJson, Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(requestMessage);
            }
        }

        public async Task<BookResponse> DeleteAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                var responseMessage = await _httpClient.SendAsync(requestMessage);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
            }
        }
    }
}
