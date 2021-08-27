using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;
using System;
using AutoMapper;

namespace WebApi.BookOperations.CreateBookCommand
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookModel Model {get;set;}

        public CreateBookCommand(BookStoreDbContext Dbcontext, IMapper mapper)
        {
            _dbContext = Dbcontext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b=>b.Title == Model.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap Zaten Mevcut!");
            book = _mapper.Map<Book>(Model);
            // book.Title = Model.Title;
            // book.GenreId = Model.GenreId;
            // book.PageCount=Model.PageCount;
            // book.PublishDate = Model.PublishDate;
            _dbContext.Add(book);
            _dbContext.SaveChanges();

        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }

    }
}
