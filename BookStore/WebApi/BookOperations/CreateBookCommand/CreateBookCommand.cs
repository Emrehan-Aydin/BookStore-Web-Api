using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;
using System;

namespace WebApi.BookOperations.CreateBookCommand
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model {get;set;}

        public CreateBookCommand(BookStoreDbContext Dbcontext)
        {
            _dbContext = Dbcontext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b=>b.Title == Model.Title);
            if(book is not null)
                throw new InvalidOperationException("Kitap Zaten Mevcut!");
            book = new Book();
            book.Title = Model.Title;
            book.GenreId = Model.GenreId;
            book.PageCount=Model.PageCount;
            book.PublishDate = Model.PublishDate;
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
