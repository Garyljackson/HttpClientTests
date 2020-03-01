using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    // Things to test/do
    // Stream data
    // Cancellation tokens

    public class TestHttpClient1
    {
        private readonly HttpClient _httpClient;

        public TestHttpClient1(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetListAsync()
        {
            var requestUri = new Uri("https://localhost:44342/api/books");
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Book>>(responseJson, DefaultJsonSerializerOptions.Options);
        }

        public async Task<Book> GetAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Book>(responseJson, DefaultJsonSerializerOptions.Options);
        }

        public async Task<Book> PostAsync()
        {
            var requestUri = new Uri("https://localhost:44342/api/books");

            var book = new
            {
                Title = "The Title",
                Author = "The Author"
            };

            var bookJson = JsonSerializer.Serialize(book);

            var httpContent = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(requestUri, httpContent);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Book>(responseJson, DefaultJsonSerializerOptions.Options);
        }

        public async Task PutAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");

            var book = new
            {
                Id = id,
                Title = "The Updated Title",
                Author = "The Updated Author"
            };

            var bookJson = JsonSerializer.Serialize(book);

            var httpContent = new StringContent(bookJson, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(requestUri, httpContent);
        }

        public async Task<Book> DeleteAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");

            var httpResponseMessage = await _httpClient.DeleteAsync(requestUri);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Book>(responseJson, DefaultJsonSerializerOptions.Options);
        }
    }
}