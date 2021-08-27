using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.RemoveBook
{
    public class RemoveBook
    {
        public BookStoreDbContext _context;
        public int id {get;set;}
        public RemoveBook(BookStoreDbContext context)
        {
            _context = context;
        }
        public void handle()
        {
            var deletedbook = _context.Books.FirstOrDefault(b=>b.Id==this.id);
            if(deletedbook is null)
                throw new InvalidOperationException("Kitap Bulunamadı!");
                
            _context.Remove(deletedbook);
            _context.SaveChanges();    
        }

    }
}