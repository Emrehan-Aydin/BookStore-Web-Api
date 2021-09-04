using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Command.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.AuthorsOperations.Command.CreateAuthor.CreateAuthor;

namespace  Application.AuthorOperations.Commands.CreateAuthorCommandTest
{
    public class CreateAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange {hazırlık}
            var author = new Author() { Name = "WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-30)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthor command = new CreateAuthor(_context,_mapper);
            command.newAuthorModel = new CreateAuthorModel()
            {
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };

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
            CreateAuthor command = new CreateAuthor(_context,_mapper);
            var author = new CreateAuthorModel() { Name = "WhenValidInputAreGiven_Book_ShouldBeCreated" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-30)};
            command.newAuthorModel = author;
            // act {Çalıştırma}
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert {Doğrulama}
            var Author = _context.Authors.SingleOrDefault(A=>
            A.Name == author.Name &&
            A.Surname == author.Surname &&
            A.DateOfBirth == author.DateOfBirth);
            Author.Should().NotBeNull();
        }
    }
}