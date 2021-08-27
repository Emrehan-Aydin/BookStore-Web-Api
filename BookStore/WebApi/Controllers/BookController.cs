using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBookCommand;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetByIdQuery;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBookCommand.CreateBookCommand;

namespace  WebApi.AddControllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetByBookId(int id)
        {
            BookModel result;
            GetByIdQuery getById = new GetByIdQuery(_context,_mapper);
            try
            {
                getById.Id = id;
                result = getById.Handle();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
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
        public IActionResult UpdateBook(int id, [FromBody] UpdatedBookModel updatedBook)
        {
            UpdateBook update = new UpdateBook(_context);
            try
            {
                update.Id = id;
                update.model = updatedBook;
                update.Handle();
                
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
           
                return Ok();
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