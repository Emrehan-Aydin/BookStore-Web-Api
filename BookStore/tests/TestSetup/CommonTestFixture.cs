using WebApi.DbOperations;
using AutoMapper;
namespace TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context {get;set;}
        public IMapper Mapper {get;set;}
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().InMemoryDatabase(databaseName:"BookStoreTestDb");
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();
        }
    }
}