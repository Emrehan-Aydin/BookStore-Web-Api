using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GerneOperation.Commands.DeleteGenre
{
    public class DeleteGenre
    {
        public int GenreId {get;set;}
        private readonly IBookStoreDbContext _context;
        public DeleteGenre(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre=>genre.Id == GenreId);
            if(genre.Name is null)
                throw new InvalidOperationException("Böyle bir kitap türü bulunamadı!");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}