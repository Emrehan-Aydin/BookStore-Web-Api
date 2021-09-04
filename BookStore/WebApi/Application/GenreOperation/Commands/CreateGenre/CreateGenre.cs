using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GerneOperation.Commands.CreateGenre
{
    public class CreateGenre
    {
        public CreateGenreModel model {get;set;}
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;
        public CreateGenre(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g=>g.Name == model.Name);
            if(genre is not null)
                throw new InvalidOperationException("Böyle bir tür zaten var.");

            genre = new Genre();
            genre.Name = model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }
    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}