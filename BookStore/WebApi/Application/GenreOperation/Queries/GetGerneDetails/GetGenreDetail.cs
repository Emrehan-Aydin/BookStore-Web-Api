using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GerneOperation.Queries.GetGenreDetails
{
    public class GetGenreDetails
    {
        public int GenreId{get;set;}
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenreDetails(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genres = _context.Genres.Where(g=>g.IsActive && g.Id == GenreId).OrderBy(g=>g.Id).SingleOrDefault();
            if(genres is null)
                throw new InvalidOperationException("Aradığınız Tür Bulunamadı!");

            return _mapper.Map<GenreDetailViewModel>(genres);
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}