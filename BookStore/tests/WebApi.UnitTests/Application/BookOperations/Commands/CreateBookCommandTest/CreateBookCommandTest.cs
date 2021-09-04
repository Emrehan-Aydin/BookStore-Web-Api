using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBookCommand;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBookCommand.CreateBookCommand;

namespace  Application.BookOperations.Commands.CreateBookCommandTest
{
    public class CreateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange {hazırlık}
            var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 200,PublishDate = new System.DateTime(1950,01,10), AuthorId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel(){Title = book.Title , AuthorId = 1 , GenreId = 1 ,PageCount = 200, PublishDate = new System.DateTime(1950,01,10)};

            // act {Çalıştırma}
            FluentActions
            .Invoking(()=>command.Handle())
            // assert {Doğrulama}
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut.");   
        }
        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            // arrange {hazırlık}
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            CreateBookModel model = new CreateBookModel(){Title = "Hobbit",AuthorId=1,GenreId=2,PageCount=3,PublishDate = DateTime.Now.Date.AddYears(-2)};
            command.Model = model;
            // act {Çalıştırma}
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert {Doğrulama}
            var book = _context.Books.SingleOrDefault(b=>b.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);


        }
    }
}