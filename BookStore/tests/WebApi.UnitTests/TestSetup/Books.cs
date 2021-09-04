using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book{
                    Title = "Lean a", GenreId = 1, AuthorId = 1, PageCount = 200,PublishDate = new DateTime(2001,06,12)
                },
                new Book{
                    Title = "Lean b",
                    GenreId = 2,
                    AuthorId = 2,
                    PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                },
                 new Book{
                    Title = "Lean c",
                    GenreId = 3,
                    AuthorId = 3,
                    PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                });       
        }

    }
}