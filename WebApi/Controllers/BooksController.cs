using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> Books = new List<Book>();


        [HttpGet]
        public ActionResult<IEnumerable<BookResponse>> Get()
        {
            var books = Books.Select(b => b.Map()).ToList();
            return books;
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<BookResponse> Get(Guid id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            return book.Map();
        }

        [HttpPost]
        public ActionResult<BookResponse> Post([FromBody] BookPostRequest postRequest)
        {
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title =postRequest.Title,
                Author = postRequest.Author
            };

            Books.Add(newBook);
            return CreatedAtAction("Get", new { id = newBook.Id }, newBook.Map());
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] BookPutRequest putRequest)
        {
            if (id != putRequest.Id) return BadRequest();

            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            var newBook = new Book
            {
                Id = putRequest.Id,
                Title = putRequest.Title,
                Author = putRequest.Author
            };

            Books.Remove(book);
            Books.Add(newBook);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<BookResponse> Delete(Guid id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            Books.Remove(book);

            return book.Map();
        }
    }
}