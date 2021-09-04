using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre{
                    Name = "Lean Startup", 
                    IsActive = true,
                },
                new Genre{
                   Name = "HerLand", 
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
        }
    }
}