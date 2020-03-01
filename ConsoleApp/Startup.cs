using System;
using System.Threading.Tasks;
using ConsoleApp.Tests;

namespace ConsoleApp
{
    internal class Startup
    {
        private readonly TestHttpClient1 _testHttpClient1;

        public Startup(TestHttpClient1 testHttpClient1)
        {
            _testHttpClient1 = testHttpClient1;
        }

        public async Task RunAsync()
        {
            var postResponse = await _testHttpClient1.PostAsync();
            
            await _testHttpClient1.PutAsync(postResponse.Id);

            var getResponse = await _testHttpClient1.GetAsync(postResponse.Id);

            var getListResponse = await _testHttpClient1.GetListAsync();
        }
    }
}