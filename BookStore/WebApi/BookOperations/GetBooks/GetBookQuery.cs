using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext Dbcontext)
        {
            _dbContext = Dbcontext;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(b=>b.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach(var book in bookList)
            {
                vm.Add(new BookViewModel(){
                Title = book.Title,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                PageCount = book.PageCount
                });
            }
            return vm;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }  
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }

    }


}