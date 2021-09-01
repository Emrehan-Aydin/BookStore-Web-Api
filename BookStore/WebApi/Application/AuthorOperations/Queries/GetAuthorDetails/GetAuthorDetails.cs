using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;


namespace WebApi.Application.AuthorsOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetails
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id { get; set; }
        public GetAuthorDetails(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorDetailModel Handle()
        {
            var obj = _context.Authors.SingleOrDefault(Au=>Au.Id == Id);
            if(obj is null)
                throw new InvalidOperationException("Aradığını yazar bulunamadı!");
            
            return _mapper.Map<AuthorDetailModel>(obj);
        }
    }
    public class AuthorDetailModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}