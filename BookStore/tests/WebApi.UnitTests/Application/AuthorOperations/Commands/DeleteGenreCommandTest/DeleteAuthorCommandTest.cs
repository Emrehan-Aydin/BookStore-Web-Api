using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Command.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthorCommandTest
{
    public class DeleteAuthorTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;

        public DeleteAuthorTest(CommonTestFixture _testFixture)
        {
            this.context = _testFixture.Context;
        }
        [Fact]
        public void WhenCanNotFound_GenreId_ShouldBeReturn()
        {
            var author = new Author { Name = "WhenCanNotFound_GenreId_ShouldBeReturn" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-30)};
            //Arrange
            context.Authors.Add(author);
            context.SaveChanges();
            context.Authors.Remove(author);
            context.SaveChanges();
            DeleteAuthor command = new DeleteAuthor(context);
            command.Id=author.Id;

            //Act

            FluentActions.Invoking(()=>command.Handle())
            //Assert

            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Böyle bir yazar Bulunamadı!");   
        }
        [Fact]
        public void WhenCanNotDeletePermission_GenreId_LiveProduct_ShouldBeReturn()
        {
            //Arrange
            var author = new Author { Name = "WhenCanNotDeletePermission_GenreId_LiveProduct_ShouldBeReturn" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-30)};
            context.Authors.Add(author);
            var authorsBook = new Book {Title = author.Name , PageCount = 200, PublishDate = DateTime.Now.Date.AddYears(-2), AuthorId = author.Id , GenreId = 1 };
            context.Books.Add(authorsBook);
            context.SaveChanges();
            DeleteAuthor command = new DeleteAuthor(context);
            command.Id=author.Id;
            //Act
            FluentActions.Invoking(()=>command.Handle())
            //Assert

            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitabı Yayında olan bir yazarı silemezsiniz!.");   
        }
        [Fact]
        public void WhenCanDeleteByAuthorId_ShouldBeReturn()
        {
            var author = new Author { Name = "WhenCanDeleteByAuthorId_ShouldBeReturn" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-30)};
            //Arrange
            context.Authors.Add(author);
            context.SaveChanges();
            DeleteAuthor command = new DeleteAuthor(context);
            command.Id=author.Id;

            //Act

            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert

            context.Genres.SingleOrDefault(b=>b.Id == author.Id).Should().BeNull();


        }
    }
}