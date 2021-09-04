using AutoMapper;
using TestSetup;
using WebApi.DbOperations;
using Xunit;
using WebApi.Application.BookOperations.Queries.GetBooks;
using System.Linq;
using FluentAssertions;

namespace Application.BookOperations.Queries.GetBooksTest
{
    public class GetBooksTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public GetBooksTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetBooksQueries_ContainEquivalentOf_ContextBookItem_ShouldBeReturn()
        {
             //arrange
            GetBooksQuery query = new GetBooksQuery(context,mapper);
            // act // assert
            FluentActions.Invoking(()=>query.Handle().Count()).Invoke().Should().Equals(context.Books.Count());
        }
        
    }
}