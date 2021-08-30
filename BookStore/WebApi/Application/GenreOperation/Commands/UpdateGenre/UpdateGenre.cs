using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GerneOperation.Commands.UpdateGenre
{
    public class UpdateGenre
    {
        public int GenreId { get; set; }
        public UpdateGenreModel model {get;set;}
        private readonly BookStoreDbContext _context;

        public UpdateGenre(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(genre=>genre.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Böyle Bir Kitap Bulunamadı!");
            if(_context.Genres.Any(x=>x.Name.ToLower() == genre.Name.ToLower() && genre.Id != GenreId))
                throw new InvalidOperationException("Güncelleştirmeye Çalıştığınız Kitap adında başka bir kitap zaten mevcut");
            genre.Name = model.Name.Trim() == default ?  genre.Name : model.Name;
            genre.IsActive = model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name {get;set;}
        public bool IsActive { get; set; }
    }
}