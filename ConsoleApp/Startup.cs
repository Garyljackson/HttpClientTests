using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Startup
    {
        public Task RunAsync()
        {
            Console.WriteLine("Hello World");
            return Task.CompletedTask;
        }
    }
}