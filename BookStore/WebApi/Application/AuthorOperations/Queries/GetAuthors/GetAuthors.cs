using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorsOperations.Queries.GetAuthors
{
    public class GetAuthors
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthors(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<AuthorVievModel> Handle()
        {
            var obj = _context.Authors.OrderBy(A=>A.Id);
            return _mapper.Map<List<AuthorVievModel>>(obj);

        }
        public class AuthorVievModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }     
            public string  DateOfBirth { get; set; }
        }
    }
}