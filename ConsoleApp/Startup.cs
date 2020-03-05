using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp.Models;
using ConsoleApp.Tests;

namespace ConsoleApp
{
    internal class Startup
    {
        private readonly HttpClientGen1 _httpClientGen1;
        private readonly HttpClientGen2 _httpClientGen2;
        private readonly HttpClientGen3 _httpClientGen3;
        private readonly HttpClientGen4 _httpClientGen4;
        private readonly HttpClientGen5 _httpClientGen5;
        private readonly HttpClientGen6 _httpClientGen6;
        private readonly HttpClientGen7 _httpClientGen7;
        private readonly HttpClientGen8 _httpClientGen8;

        public Startup(
            HttpClientGen1 httpClientGen1, HttpClientGen2 httpClientGen2, HttpClientGen3 httpClientGen3, HttpClientGen4 httpClientGen4, 
            HttpClientGen5 httpClientGen5, HttpClientGen6 httpClientGen6, HttpClientGen7 httpClientGen7, HttpClientGen8 httpClientGen8)
        {
            _httpClientGen1 = httpClientGen1;
            _httpClientGen2 = httpClientGen2;
            _httpClientGen3 = httpClientGen3;
            _httpClientGen4 = httpClientGen4;
            _httpClientGen5 = httpClientGen5;
            _httpClientGen6 = httpClientGen6;
            _httpClientGen7 = httpClientGen7;
            _httpClientGen8 = httpClientGen8;
        }

        public async Task RunAsync()
        {
            await TestClient(_httpClientGen1, "The Title", "The Author");
            await TestClient(_httpClientGen2, "The Title", "The Author");
            await TestClient(_httpClientGen3, "The Title", "The Author");
            await TestClient(_httpClientGen4, "The Title", "The Author");
            await TestClient(_httpClientGen5, "The Title", "The Author");
            await TestClient6(_httpClientGen6, "The Title", "The Author");
            await TestClient7(_httpClientGen7, "The Title", "The Author");
            await TestClient8(_httpClientGen8, "The Title", "The Author");
        }

        private async Task TestClient(ITestHttpClient testHttpClient, string title, string author)
        {
            var postResponse = await testHttpClient.PostAsync(new BookPostRequest
            {
                Title = title,
                Author = author
            });

            await testHttpClient.PutAsync(new BookPutRequest
            {
                Id = postResponse.Id,
                Title = $"{title} - updated" ,
                Author = $"{author} - updated"
            });

            var getResponse = await testHttpClient.GetAsync(postResponse.Id);

            var getListResponse = await testHttpClient.GetListAsync();

            var deleteResponse = await testHttpClient.DeleteAsync(postResponse.Id);
        }

        private async Task TestClient6(HttpClientGen6 httpClientGen6, string title, string author)
        {
            var postResponse = await httpClientGen6.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await httpClientGen6.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await httpClientGen6.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await httpClientGen6.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await httpClientGen6.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }

        private async Task TestClient7(HttpClientGen7 httpClientGen7, string title, string author)
        {
            var postResponse = await httpClientGen7.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await httpClientGen7.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await httpClientGen7.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await httpClientGen7.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await httpClientGen7.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }

        private async Task TestClient8(HttpClientGen8 httpClientGen8, string title, string author)
        {
            var postResponse = await httpClientGen8.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await httpClientGen8.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await httpClientGen8.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await httpClientGen8.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await httpClientGen8.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }

    }
}