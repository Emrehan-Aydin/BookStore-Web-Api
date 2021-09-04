using AutoMapper;
using TestSetup;
using WebApi.DbOperations;
using Xunit;
using System.Linq;
using FluentAssertions;

using WebApi.Application.AuthorsOperations.Queries.GetAuthorDetails;
using WebApi.Entities;
using WebApi.Application.AuthorsOperations.Queries.GetAuthors;

namespace Application.AuthorOperations.Queries.GetAuthorTest
{
    public class GetAuthorsTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public GetAuthorsTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetBooksQueries_ContainEquivalentOf_ContextBookItem_ShouldBeReturn()
        {
             //arrange
            GetAuthors query = new GetAuthors(context,mapper);
            // act // assert
            FluentActions.Invoking(()=>query.Handle().Count()).Invoke().Should().Equals(context.Authors.Count());
        }

    }
}