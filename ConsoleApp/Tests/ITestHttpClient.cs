using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp.Models;

namespace ConsoleApp.Tests
{
    public interface ITestHttpClient
    {
        Task<IEnumerable<BookResponse>> GetListAsync();
        Task<BookResponse> GetAsync(Guid id);
        Task<BookResponse> PostAsync(BookPostRequest request);
        Task PutAsync(BookPutRequest request);
        Task<BookResponse> DeleteAsync(Guid id);
    }
}