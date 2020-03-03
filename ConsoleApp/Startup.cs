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

        public Startup(TestHttpClient1 testHttpClient1, TestHttpClient2 testHttpClient2, TestHttpClient3 testHttpClient3, TestHttpClient4 testHttpClient4, TestHttpClient5 testHttpClient5)
        {
            _testHttpClient1 = testHttpClient1;
            _testHttpClient2 = testHttpClient2;
            _testHttpClient3 = testHttpClient3;
            _testHttpClient4 = testHttpClient4;
            _testHttpClient5 = testHttpClient5;
        }

        public async Task RunAsync()
        {
            //await TestClient(_testHttpClient1, "The Title", "The Author");
            //await TestClient(_testHttpClient2, "The Title", "The Author");
            //await TestClient(_testHttpClient3, "The Title", "The Author");
            //await TestClient(_testHttpClient4, "The Title", "The Author");
            await TestClient(_testHttpClient5, "The Title", "The Author");
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
    }
}