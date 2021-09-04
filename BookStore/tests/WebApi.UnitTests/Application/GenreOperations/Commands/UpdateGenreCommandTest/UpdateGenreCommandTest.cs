using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBookCommandTest
{
    public class UpdateGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidUpdatedDataInput_IsItRecorded()
        {
            //arrange
            UpdateGenre Command = new UpdateGenre(context);
            var genre = new Genre {Id = 444 , Name = "NoUpdatedData" , IsActive = true};
            context.Genres.Add(genre);
            var updatedBook= new UpdateGenreModel{ Name = "YesUpdatedData" , IsActive = false};
            Command.GenreId = genre.Id;
            Command.model = updatedBook;
            context.SaveChanges();
            // act
            FluentActions.Invoking(() => Command.Handle()).Invoke();
            //assert 
            var GetUpdatedGenre = context.Genres.SingleOrDefault(b=>b.Id==genre.Id);
            GetUpdatedGenre.Name.Should().Be(updatedBook.Name);
            GetUpdatedGenre.IsActive.Should().Be(updatedBook.IsActive);
        }
    
    }
}