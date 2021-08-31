using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorsOperations.Command.DeleteAuthor
{
    public class DeleteAuthor
    {
        public int Id { get; set; }

        private readonly BookStoreDbContext _context;
        public DeleteAuthor(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle() 
        {
            var deleteAuthor = _context.Authors.SingleOrDefault(Aut=>Aut.Id == Id);
            if(deleteAuthor is null)
                throw new InvalidOperationException("Böyle bir yazar Bulunamadı!");
            if(_context.Books.FirstOrDefault(B=>B.AuthorId==deleteAuthor.Id) is not null)
                throw new InvalidOperationException("Kitabı Yayında olan bir yazarı silemezsiniz!.");
            _context.Remove(deleteAuthor);
            _context.SaveChanges();
        }

    }
    
}