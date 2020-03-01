using WebApi.Models;

namespace WebApi.Extensions
{
    public static class BookModelExtensions
    {
        public static BookResponse Map(this Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
        }
    }
}