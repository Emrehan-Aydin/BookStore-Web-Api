using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Queries.GetGenreDetails;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetailsTest
{
    public class  GetGenreDetailsTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public GetGenreDetailsTests(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInputGenreId_NotFound_ShouldBeReturn_Exception()
        {
            //arrange
            var genre = new Genre{ Name = "WhenInputGenreId_NotFound_ShouldBeReturn_Exception"};
            context.Genres.Add(genre);
            context.SaveChanges();
            context.Genres.Remove(genre);
            context.SaveChanges();

            GetGenreDetails command = new GetGenreDetails(context,mapper);
            command.GenreId=genre.Id;
            // Act and Assert
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aradığınız Tür Bulunamadı!");

        }
        [Fact]
        public void WhenValidInputGenreId_GetDetails_ShouldEqualDatas()
        {
            //arrange
            var genre = new Genre{ Name = "WhenValidInputGenreId_GetDetails_ShouldEqualDatas"};
            context.Genres.Add(genre);
            context.SaveChanges();
            GetGenreDetails command = new GetGenreDetails(context,mapper);
            command.GenreId=genre.Id;
            // Act and Assert
            FluentActions.Invoking(()=>command.Handle());
            var genreDetail = context.Genres.SingleOrDefault(G=>G.Id==genre.Id);
            genreDetail.Name.Should().Be(genre.Name);
            genreDetail.IsActive.Should().Be(genre.IsActive);

        }
    }
}