using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context {get;set;}
        public IMapper Mapper {get;set;}
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddBooks();
            
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg=>{cfg.AddProfile<MappingProfile>();}).CreateMapper();
            

        }

    }
}