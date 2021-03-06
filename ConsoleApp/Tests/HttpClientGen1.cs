﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public class HttpClientGen1 : ITestHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientGen1(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookResponse>> GetListAsync()
        {
            var requestUri = new Uri("https://localhost:44342/api/books");
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BookResponse>>(responseJson, DefaultJsonSerializerOptions.Options);
        }

        public async Task<BookResponse> GetAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");
            var httpResponseMessage = await _httpClient.GetAsync(requestUri);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
        }

        public async Task<BookResponse> PostAsync(BookPostRequest request)
        {
            var requestUri = new Uri("https://localhost:44342/api/books");

            var bookJson = JsonSerializer.Serialize(request);

            var httpContent = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(requestUri, httpContent);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
        }

        public async Task PutAsync(BookPutRequest request)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{request.Id}");

            var bookJson = JsonSerializer.Serialize(request);

            var httpContent = new StringContent(bookJson, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(requestUri, httpContent);
        }

        public async Task<BookResponse> DeleteAsync(Guid id)
        {
            var requestUri = new Uri($"https://localhost:44342/api/books/{id}");

            var httpResponseMessage = await _httpClient.DeleteAsync(requestUri);
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BookResponse>(responseJson, DefaultJsonSerializerOptions.Options);
        }
    }
}