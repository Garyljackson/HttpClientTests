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

        public Startup(TestHttpClient1 testHttpClient1, TestHttpClient2 testHttpClient2, TestHttpClient3 testHttpClient3, TestHttpClient4 testHttpClient4)
        {
            _testHttpClient1 = testHttpClient1;
            _testHttpClient2 = testHttpClient2;
            _testHttpClient3 = testHttpClient3;
            _testHttpClient4 = testHttpClient4;
        }

        public async Task RunAsync()
        {
            await Test1("The Title", "The Author");
            await Test2("The Title", "The Author");
            await Test3("The Title", "The Author");
            await Test4("The Title", "The Author");
        }

        private async Task Test1(string title, string author)
        {
            var postResponse = await _testHttpClient1.PostAsync(new BookPostRequest
            {
                Title = title,
                Author = author
            });

            await _testHttpClient1.PutAsync(new BookPutRequest
            {
                Id = postResponse.Id,
                Title = $"{title} - updated" ,
                Author = $"{author} - updated"
            });

            var getResponse = await _testHttpClient1.GetAsync(postResponse.Id);

            var getListResponse = await _testHttpClient1.GetListAsync();

            var deleteResponse = await _testHttpClient1.DeleteAsync(postResponse.Id);
        }

        private async Task Test2(string title, string author)
        {
            var postResponse = await _testHttpClient2.PostAsync(new BookPostRequest
            {
                Title = title,
                Author = author
            });

            await _testHttpClient2.PutAsync(new BookPutRequest
            {
                Id = postResponse.Id,
                Title = $"{title} - updated",
                Author = $"{author} - updated"
            });

            var getResponse = await _testHttpClient2.GetAsync(postResponse.Id);

            var getListResponse = await _testHttpClient2.GetListAsync();

            var deleteResponse = await _testHttpClient2.DeleteAsync(postResponse.Id);
        }

        private async Task Test3(string title, string author)
        {
            var postResponse = await _testHttpClient3.PostAsync(new BookPostRequest
            {
                Title = title,
                Author = author
            });

            await _testHttpClient3.PutAsync(new BookPutRequest
            {
                Id = postResponse.Id,
                Title = $"{title} - updated",
                Author = $"{author} - updated"
            });

            var getResponse = await _testHttpClient3.GetAsync(postResponse.Id);

            var getListResponse = await _testHttpClient3.GetListAsync();

            var deleteResponse = await _testHttpClient3.DeleteAsync(postResponse.Id);
        }

        private async Task Test4(string title, string author)
        {
            var postResponse = await _testHttpClient4.PostAsync(new BookPostRequest
            {
                Title = title,
                Author = author
            });

            await _testHttpClient4.PutAsync(new BookPutRequest
            {
                Id = postResponse.Id,
                Title = $"{title} - updated",
                Author = $"{author} - updated"
            });

            var getResponse = await _testHttpClient4.GetAsync(postResponse.Id);

            var getListResponse = await _testHttpClient4.GetListAsync();

            var deleteResponse = await _testHttpClient4.DeleteAsync(postResponse.Id);
        }
    }
}