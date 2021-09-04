using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace  Application.GenreOperations.Commands.CreateGenreCommandTest
{
    public class CreateGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange {hazırlık}
            var genre = new Genre() { Name = "WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn" , IsActive = true};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenre command = new CreateGenre(_context,_mapper);
            command.model = new CreateGenreModel(){Name = genre.Name};

            // act {Çalıştırma}
            FluentActions
            .Invoking(()=>command.Handle())
            // assert {Doğrulama}
            .Should().Throw<InvalidOperationException>();  
        }
        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            // arrange {hazırlık}
            CreateGenre command = new CreateGenre(_context,_mapper);
            var model = new CreateGenreModel(){Name = "WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn"};
            command.model = model;
            // act {Çalıştırma}
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert {Doğrulama}
            var genre = _context.Genres.SingleOrDefault(b=>b.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            


        }
    }
}