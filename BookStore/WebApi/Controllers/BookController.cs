using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBookCommand;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetByIdQuery;
using WebApi.BookOperations.RemoveBook;
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
            getById.Id = id;
            GetByIdQueryValidation validation = new GetByIdQueryValidation();
            validation.ValidateAndThrow(getById);
            result = getById.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = newBook;
            CreateBookCOmmandValidator validator = new CreateBookCOmmandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Başarıyla Eklendi");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedBookModel updatedBook)
        {
            UpdateBook update = new UpdateBook(_context);
            update.Id = id;
            update.model = updatedBook;
            UpdateBookValidation validation = new UpdateBookValidation();
            validation.ValidateAndThrow(update);
            update.Handle();  
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            RemoveBook delete = new RemoveBook(_context);
            delete.id=id;
            RemoveBookValidation validator = new RemoveBookValidation();
            validator.ValidateAndThrow(delete);
            delete.handle();    
            return Ok();
        }
    }  
}