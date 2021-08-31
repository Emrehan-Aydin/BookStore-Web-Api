using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations{
    public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }
            context.Genres.AddRange(
                new Genre{
                    Name = "Lean Startup", 
                    IsActive = true,
                },
                new Genre{
                   Name = "HerLnad", 
                   IsActive = true,
               },
                new Genre{
                   Name = "Romance", 
                   IsActive = true,
               },
                new Genre{
                   Name = "Stories", 
                   IsActive = true,
               });
            
            context.Books.AddRange(
                new Book{
                //Id = 1,
                    Title = "Lean a",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                },
                new Book{
                //Id = 2,
                    Title = "Lean b",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                },
                 new Book{
                //Id = 3,
                    Title = "Lean c",
                    GenreId = 1,
                     PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                });

            context.Authors.AddRange(
                new Author{
                    Name = "Ali",
                    Surname = "Kemal",
                    DateOfBirth = new DateTime(1976,08,11)
                    
                },
                 new Author{
                    Name = "Mehmet",
                    Surname = "Akif",
                    DateOfBirth = new DateTime(1967,12,1)
                    
                },
                 new Author{
                    Name = "Burak",
                    Surname = "Koç",
                    DateOfBirth = new DateTime(1984,3,24)
                });
                
                context.SaveChanges();
            }
        }
    }
}