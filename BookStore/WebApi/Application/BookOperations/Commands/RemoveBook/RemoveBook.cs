using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.RemoveBook
{
    public class RemoveBook
    {
        public IBookStoreDbContext _context;
        public int id {get;set;}
        public RemoveBook(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void handle()
        {
            var deletedbook = _context.Books.FirstOrDefault(b=>b.Id==this.id);
            if(deletedbook is null)
                throw new InvalidOperationException("Kitap Bulunamadı!");
                
            _context.Books.Remove(deletedbook);
            _context.SaveChanges();    
        }

    }
}