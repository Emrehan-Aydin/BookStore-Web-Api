using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetByIdQuery
{
    public class BookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }  
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author {get;set;}
    }
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private IMapper _mapper;
        public int Id { get; set; }

        public GetByIdQuery(BookStoreDbContext Dbcontext,IMapper mapper)
        {
            _dbContext = Dbcontext;
            _mapper = mapper;
        }

        public BookModel Handle()
        {
            var book = _dbContext.Books.Include(b=>b.Genre).Include(b=>b.Author).Where(book=> book.Id==this.Id).FirstOrDefault();
            if(book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            BookModel model = _mapper.Map<BookModel>(book);
            return model;

        }
        
    }
}
