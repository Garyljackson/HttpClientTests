using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp.Models;
using ConsoleApp.Tests;

namespace ConsoleApp
{
    internal class Startup
    {
        private readonly Gen1HttpClient _gen1HttpClient;
        private readonly Gen2HttpClient _gen2HttpClient;
        private readonly Gen3HttpClient _gen3HttpClient;
        private readonly Gen4HttpClient _gen4HttpClient;
        private readonly Gen5HttpClient _gen5HttpClient;
        private readonly Gen6HttpClient _gen6HttpClient;
        private readonly Gen7HttpClient _gen7HttpClient;

        public Startup(
            Gen1HttpClient gen1HttpClient, Gen2HttpClient gen2HttpClient, Gen3HttpClient gen3HttpClient, Gen4HttpClient gen4HttpClient, 
            Gen5HttpClient gen5HttpClient, Gen6HttpClient gen6HttpClient, Gen7HttpClient gen7HttpClient)
        {
            _gen1HttpClient = gen1HttpClient;
            _gen2HttpClient = gen2HttpClient;
            _gen3HttpClient = gen3HttpClient;
            _gen4HttpClient = gen4HttpClient;
            _gen5HttpClient = gen5HttpClient;
            _gen6HttpClient = gen6HttpClient;
            _gen7HttpClient = gen7HttpClient;
        }

        public async Task RunAsync()
        {
            await TestClient(_gen1HttpClient, "The Title", "The Author");
            await TestClient(_gen2HttpClient, "The Title", "The Author");
            await TestClient(_gen3HttpClient, "The Title", "The Author");
            await TestClient(_gen4HttpClient, "The Title", "The Author");
            await TestClient(_gen5HttpClient, "The Title", "The Author");
            await TestClient6(_gen6HttpClient, "The Title", "The Author");
            await TestClient7(_gen7HttpClient, "The Title", "The Author");
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

        private async Task TestClient6(Gen6HttpClient gen6HttpClient, string title, string author)
        {
            var postResponse = await gen6HttpClient.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await gen6HttpClient.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await gen6HttpClient.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await gen6HttpClient.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await gen6HttpClient.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }

        private async Task TestClient7(Gen7HttpClient gen7HttpClient, string title, string author)
        {
            var postResponse = await gen7HttpClient.PostAsync<BookPostRequest, BookResponse>(
                new Uri("/api/books", UriKind.Relative),
                new BookPostRequest
                {
                    Title = title,
                    Author = author
                });

            await gen7HttpClient.PutAsync(
                new Uri($"/api/books/{postResponse.Id}", UriKind.Relative),
                new BookPutRequest
                {
                    Id = postResponse.Id,
                    Title = $"{title} - updated",
                    Author = $"{author} - updated"
                });

            var getResponse = await gen7HttpClient.GetAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));

            var getListResponse = await gen7HttpClient.GetAsync<IEnumerable<BookResponse>>(new Uri("/api/books", UriKind.Relative));

            var deleteResponse = await gen7HttpClient.DeleteAsync<BookResponse>(new Uri($"/api/books/{postResponse.Id}", UriKind.Relative));
        }



    }
}