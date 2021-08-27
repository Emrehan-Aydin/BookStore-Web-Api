using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetByIdQuery
{
    public class BookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }  
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public BookModel model {get;set;}
        public int Id { get; set; }

        public GetByIdQuery(BookStoreDbContext Dbcontext)
        {
            _dbContext = Dbcontext;
        }

        public BookModel Handle()
        {
            var book = _dbContext.Books.Where(book=> book.Id==this.Id).FirstOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            else
            {
                model.Title = book.Title;
                model.Genre = ((GenreEnum)book.GenreId).ToString();
                model.PageCount = book.PageCount;
                model.PublishDate = book.PublishDate.ToString("dd/MM/yyy");
                return model;
            }  
        }
        
    }
}
