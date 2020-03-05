using System.Net.Http;

namespace ConsoleApp.Tests
{
    public class HttpClientGen8 : BaseHttpClient
    {
        public HttpClientGen8(HttpClient httpClient) : base(httpClient, DefaultJsonSerializerOptions.Options)
        {
        }
    }
}