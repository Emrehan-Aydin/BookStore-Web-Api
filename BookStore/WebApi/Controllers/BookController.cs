using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBookCommand;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBookCommand.CreateBookCommand;

namespace  WebApi.AddControllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public Book GetByBookId(int id)
        {
            var bookList = _context.Books.Where(book=> book.Id==id).SingleOrDefault();
            return bookList;
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok("Başarıyla Eklendi");
            
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(b=>b.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            else
            {
                book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
                book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
                book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
                book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
                _context.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deletedbook = _context.Books.SingleOrDefault(b=>b.Id==id);
            if(deletedbook is null)
            {
                return BadRequest();
            }
            else
            {
                _context.Books.Remove(deletedbook);
                _context.SaveChanges();
                return Ok();
            }
        }
    }  
}