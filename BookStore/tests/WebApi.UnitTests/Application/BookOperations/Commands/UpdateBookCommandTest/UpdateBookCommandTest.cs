using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBookCommandTest
{
    public class UpdateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidUpdatedDataInput_IsItRecorded()
        {
            //arrange
            UpdateBook Command = new UpdateBook(context);
            var book = new Book {Id = 444 , AuthorId = 1 , GenreId = 1 , PageCount = 1 , Title = "NoUpdatedBook", PublishDate = DateTime.Now.Date.AddYears(-10)};
            context.Books.Add(book);
            var updatedBook= new UpdatedBookModel{ Title = "YesUpdatedBook" ,AuthorId= 2,GenreId= 3,PageCount =200,PublishDate= DateTime.Now.Date.AddYears(5)};
            Command.Id = book.Id;
            Command.model = updatedBook;
            context.SaveChanges();
            // act
            FluentActions.Invoking(() => Command.Handle()).Invoke();
            //assert 
            var GetUpdatedBook = context.Books.SingleOrDefault(b=>b.Id==book.Id);
            GetUpdatedBook.AuthorId.Should().Be(updatedBook.AuthorId);
            GetUpdatedBook.GenreId.Should().Be(updatedBook.GenreId);
            GetUpdatedBook.PageCount.Should().Be(updatedBook.PageCount);
            GetUpdatedBook.Title.Should().Be(updatedBook.Title);
            GetUpdatedBook.PublishDate.Should().Be(updatedBook.PublishDate);
        }
    
    }
}