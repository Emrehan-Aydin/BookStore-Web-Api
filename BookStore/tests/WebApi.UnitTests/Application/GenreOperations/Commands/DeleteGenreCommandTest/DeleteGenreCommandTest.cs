using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenreCommandTest
{
    public class DeleteGenreTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;

        public DeleteGenreTest(CommonTestFixture _testFixture)
        {
            this.context = _testFixture.Context;
        }
        [Fact]
        public void WhenCanNotFound_GenreId_ShouldBeReturn()
        {
            var genre = new Genre {Name = "WhenCanNotFound_GenreId_ShouldBeReturn", IsActive = true};
            //Arrange
            context.Genres.Add(genre);
            context.SaveChanges();
            context.Genres.Remove(genre);
            context.SaveChanges();
            DeleteGenre command = new DeleteGenre(context);
            command.GenreId=genre.Id;

            //Act

            FluentActions.Invoking(()=>command.Handle())
            //Assert

            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Böyle bir kitap türü bulunamadı!");   
        }
        [Fact]
        public void WhenCanDeleteByGenreId_ShouldBeReturn()
        {
            var genre = new Genre {Name = "WhenCanNotFound_GenreId_ShouldBeReturn"};
            //Arrange
            context.Genres.Add(genre);
            context.SaveChanges();
            DeleteGenre command = new DeleteGenre(context);
            command.GenreId=genre.Id;

            //Act

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert

            context.Genres.SingleOrDefault(b=>b.Id == genre.Id).Should().BeNull();


        }
    }
}