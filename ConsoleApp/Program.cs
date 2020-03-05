using System;
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
            services.AddHttpClient<HttpClientGen1>();
            services.AddHttpClient<HttpClientGen2>();
            services.AddHttpClient<HttpClientGen3>();
            services.AddHttpClient<HttpClientGen4>();
            services.AddHttpClient<HttpClientGen5>();
            services.AddHttpClient<HttpClientGen6>();
            services.AddHttpClient<HttpClientGen7>();
            services.AddHttpClient<HttpClientGen8>(client => client.BaseAddress = new Uri("https://localhost:44342/"));
        }
    }
}