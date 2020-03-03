using System.Threading.Tasks;
using ConsoleApp.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();

            var startup = host.Services.GetRequiredService<Startup>();
            await startup.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services)
                    => RegisterServices(services, hostContext.Configuration));
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddSingleton<Startup>();

            // Http Clients
            services.AddHttpClient<TestHttpClient1>();
            services.AddHttpClient<TestHttpClient2>();
            services.AddHttpClient<TestHttpClient3>();
            services.AddHttpClient<TestHttpClient4>();
        }
    }
}