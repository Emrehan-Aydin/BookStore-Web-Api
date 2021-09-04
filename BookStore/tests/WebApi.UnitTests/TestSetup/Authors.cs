using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
            
        }
    }
}