using System.Threading.Tasks;
using ConsoleApp.Models;
using ConsoleApp.Tests;

namespace ConsoleApp
{
    internal class Startup
    {
        private readonly TestHttpClient1 _testHttpClient1;
        private readonly TestHttpClient2 _testHttpClient2;

        public Startup(TestHttpClient1 testHttpClient1, TestHttpClient2 testHttpClient2)
        {
            _testHttpClient1 = testHttpClient1;
            _testHttpClient2 = testHttpClient2;
        }

        public async Task RunAsync()
        {
            await Test1("The Title", "The Author");
            await Test2("The Title", "The Author");
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

            var deleteResponse = await _testHttpClient1.DeleteAsync(postResponse.Id);
        }
    }
}