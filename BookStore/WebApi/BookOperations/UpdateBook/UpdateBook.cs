using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class UpdatedBookModel
    {
        public int Id {get;set;}
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
    public class UpdateBook
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdatedBookModel model;

        public UpdateBook(BookStoreDbContext Dbcontext)
        {
            _dbContext = Dbcontext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b=>b.Id == model.Id);
            if (book is null)
            {
                throw new InvalidOperationException("Güncellenecek bir kitap bulunamadı!");
            }
            else
            {
                book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
                book.PageCount = model.PageCount != default ? model.PageCount : book.PageCount;
                book.PublishDate = model.PublishDate != default ? model.PublishDate : book.PublishDate;
                book.Title = model.Title != default ? model.Title : book.Title;
                _dbContext.SaveChanges();
            }
        }
    }
}