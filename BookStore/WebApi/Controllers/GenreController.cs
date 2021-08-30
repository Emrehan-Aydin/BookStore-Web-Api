using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GerneOperation.Commands.CreateGenre;
using WebApi.Application.GerneOperation.Commands.DeleteGenre;
using WebApi.Application.GerneOperation.Commands.UpdateGenre;
using WebApi.Application.GerneOperation.Queries.GetGenreDetails;
using WebApi.Application.GerneOperation.Queries.GetGenres;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGernes()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var obj = query.Handle();
            return Ok(obj);
        }        

        [HttpGet("{id}")]
        public IActionResult GetGerneDetails(int id)
        {
            GetGenreDetails query = new GetGenreDetails(_context,_mapper);
            query.GenreId = id;
            GetGenreDetailsValidator validator = new GetGenreDetailsValidator();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }       
        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenre createGenre = new CreateGenre(_context,_mapper);
            createGenre.model = newGenre;

            CreateGenreValidator validator = new CreateGenreValidator();
            validator.ValidateAndThrow(createGenre);

            createGenre.Handle();
            return Ok();
        }  
        [HttpPut("{id}")]  
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updatedGenre) 
        {
            UpdateGenre updateGenre = new UpdateGenre(_context);
            updateGenre.GenreId = id;
            updateGenre.model = updatedGenre;

            UpdateGenreValidator validator = new UpdateGenreValidator();
            validator.ValidateAndThrow(updateGenre);

            updateGenre.Handle();
            return Ok();
            
        }  

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenre deleteGenre = new DeleteGenre(_context);
            deleteGenre.GenreId = id;

            DeleteGenreValidator validator = new DeleteGenreValidator();
            validator.ValidateAndThrow(deleteGenre);

            deleteGenre.Handle();
            return Ok();
        }
    }
}