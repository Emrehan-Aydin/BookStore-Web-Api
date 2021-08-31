using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorsOperations.Command.UpdateAuthor
{
    public class UpdateAuthor
    {
        private readonly BookStoreDbContext _context;
        public UpdatedAuthorModel updatedAuthor { get; set; }
        public int Id { get; set; }

        public UpdateAuthor(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(Aut=>Aut.Id == Id);
            if(author is null)
                throw new InvalidOperationException();

            author.Name = updatedAuthor.Name.Trim() == default? author.Name: updatedAuthor.Name;
            author.Surname = updatedAuthor.Surname.Trim() == default? author.Surname: updatedAuthor.Surname;
            author.DateOfBirth = updatedAuthor.DateOfBirth == default? author.DateOfBirth: updatedAuthor.DateOfBirth;
            _context.SaveChanges();

        }
        public class UpdatedAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }

        }
    }
}