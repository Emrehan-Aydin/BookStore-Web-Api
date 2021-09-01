using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorsOperations.Command.CreateAuthor;
using WebApi.Application.AuthorsOperations.Command.DeleteAuthor;
using WebApi.Application.AuthorsOperations.Command.UpdateAuthor;
using WebApi.Application.AuthorsOperations.Queries.GetAuthorDetails;
using WebApi.Application.AuthorsOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using static WebApi.Application.AuthorsOperations.Command.CreateAuthor.CreateAuthor;
using static WebApi.Application.AuthorsOperations.Command.UpdateAuthor.UpdateAuthor;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthors query = new GetAuthors(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthors(int id)
        {
            GetAuthorDetails query = new GetAuthorDetails(_context,_mapper);
            query.Id = id;
            GetAuthorDetailsValidator validator = new GetAuthorDetailsValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthor query = new DeleteAuthor(_context);
            query.Id = id;
            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthor query = new CreateAuthor(_context,_mapper);
            query.newAuthorModel = newAuthor;
            CreateAuthorValidator validator = new CreateAuthorValidator();
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor([FromBody] UpdatedAuthorModel updateAuthor,int id)
        {
            UpdateAuthor query = new UpdateAuthor(_context);
            query.Id = id;
            query.updatedAuthor= updateAuthor;
            UpdateAuthorValidator validator = new UpdateAuthorValidator();
            validator.ValidateAndThrow(query);
            query.Handle();
            return Ok();
        }
    }
}