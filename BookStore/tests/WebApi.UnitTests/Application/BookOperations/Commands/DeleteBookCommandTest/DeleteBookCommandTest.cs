using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.RemoveBook;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBookCommandTest
{
    public class DeleteBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;

        public DeleteBookCommandTest(CommonTestFixture _testFixture)
        {
            this.context = _testFixture.Context;
        }
        [Fact]
        public void WhenCanNotFound_BookId_ShouldBeReturn()
        {
            var book = new Book { Id = 404 ,Title = "WhenCanNotFound_BookId_ShouldBeReturn", PageCount = 200,PublishDate = new System.DateTime(1950,01,10), AuthorId = 1};
            //Arrange
            context.Books.Add(book);
            context.Books.Remove(book);
            context.SaveChanges();
            RemoveBook command = new RemoveBook(context);
            command.id=404;

            //Act

            FluentActions.Invoking(()=>command.handle())
            //Assert

            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı!");   
        }
        [Fact]
        public void WhenCanDeleteByBookId_ShouldBeReturn()
        {
            var book = new Book { Id = 404 ,Title = "WhenCanNotFound_BookId_ShouldBeReturn", PageCount = 200,PublishDate = new System.DateTime(1950,01,10), AuthorId = 1};
            //Arrange
            context.Books.Add(book);
            context.SaveChanges();
            RemoveBook command = new RemoveBook(context);
            command.id=book.Id;

            //Act

            FluentActions.Invoking(()=>command.handle()).Invoke();
            //Assert

            context.Books.SingleOrDefault(b=>b.Id == book.Id).Should().BeNull();


        }
    }
}