using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GerneOperation.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(g=>g.IsActive).OrderBy(g=>g.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
}