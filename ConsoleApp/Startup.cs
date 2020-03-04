using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp.Models;
using ConsoleApp.Tests;

namespace ConsoleApp
{
    internal class Startup
    {
        private readonly TestHttpClient1 _testHttpClient1;
        private readonly TestHttpClient2 _testHttpClient2;
        private readonly TestHttpClient3 _testHttpClient3;
        private readonly TestHttpClient4 _testHttpClient4;
        private readonly TestHttpClient5 _testHttpClient5;
        private readonly TestHttpClient6 _testHttpClient6;
        private readonly TestHttpClient7 _testHttpClient7;

        public Startup(
            TestHttpClient1 testHttpClient1, TestHttpClient2 testHttpClient2, TestHttpClient3 testHttpClient3, TestHttpClient4 testHttpClient4, 
            TestHttpClient5 testHttpClient5, TestHttpClient6 testHttpClient6, TestHttpClient7 testHttpClient7)
        {
            _testHttpClient1 = testHttpClient1;
            _testHttpClient2 = testHttpClient2;
            _testHttpClient3 = testHttpClient3;
            _testHttpClient4 = testHttpClient4;
            _testHttpClient5 = testHttpClient5;
            _testHttpClient6 = testHttpClient6;
            _testHttpClient7 = testHttpClient7;
        }

        public async Task RunAsync()
        {
            //await TestClient(_testHttpClient1, "The Title", "The Author");
            //await TestClient(_testHttpClient2, "The Title", "The Author");
            //await TestClient(_testHttpClient3, "The Title", "The Author");
            //await TestClient(_testHttpClient4, "The Title", "The Author");
            //await TestClient(_testHttpClient5, "The Title", "The Author");
            //await TestClient6(_testHttpClient6, "The Title", "The Author");
            await TestClient7(_testHttpClient7, "The Title", "The Author");
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

        private async Task TestClient6(TestHttpClient6 testHttpClient, string title, string author)
        {
            var postResponse = await testHttpClient.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await testHttpClient.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await testHttpClient.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await testHttpClient.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await testHttpClient.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }

        private async Task TestClient7(TestHttpClient7 testHttpClient, string title, string author)
        {
            var postResponse = await testHttpClient.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await testHttpClient.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await testHttpClient.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await testHttpClient.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await testHttpClient.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }



    }
}