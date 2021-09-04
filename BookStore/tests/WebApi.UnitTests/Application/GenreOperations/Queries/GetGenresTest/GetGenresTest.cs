using AutoMapper;
using TestSetup;
using WebApi.DbOperations;
using Xunit;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GerneOperation.Queries.GetGenres;

namespace Application.GenreOperations.Queries.GetGenresTest
{
    public class GetGenresTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public GetGenresTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetBooksQueries_ContainEquivalentOf_ContextBookItem_ShouldBeReturn()
        {
             //arrange
            GetGenresQuery query = new GetGenresQuery(context,mapper);
            // act // assert
            FluentActions.Invoking(()=>query.Handle().Count()).Invoke().Should().Equals(context.Genres.Count());
        }

    }
}