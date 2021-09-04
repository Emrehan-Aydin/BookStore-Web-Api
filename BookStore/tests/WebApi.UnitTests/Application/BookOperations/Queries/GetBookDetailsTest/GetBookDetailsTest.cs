using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetByIdQuery;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetailsTest
{
    public class  GetBookDetailsTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public GetBookDetailsTests(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInputBookId_NotFound_ShouldBeReturn_Exception()
        {
            //arrange
            var book = new Book{ PageCount = 200, PublishDate = DateTime.Now.Date.AddYears(-1),GenreId= 1,AuthorId=1,Title = "WhenInputBookId_NotFound_ShouldBeReturn_Exception"};
            context.Books.Add(book);
            context.SaveChanges();
            context.Books.Remove(book);
            context.SaveChanges();

            GetByIdQuery command = new GetByIdQuery(context,mapper);
            command.Id=book.Id;
            // Act and Assert
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");

        }
        [Fact]
        public void WhenValidInputBookId_GetDetails_ShouldEqualDatas()
        {
            //arrange
             //arrange
            var book = new Book{ PageCount = 200, PublishDate = DateTime.Now.Date.AddYears(-1),GenreId= 1,AuthorId=1,Title = "WhenInputBookId_NotFound_ShouldBeReturn_Exception"};
            context.Books.Add(book);
            context.SaveChanges();
           GetByIdQuery command = new GetByIdQuery(context,mapper);
            command.Id=book.Id;
            // Act and Assert
            FluentActions.Invoking(()=>command.Handle());
            var bookDetail = context.Books.SingleOrDefault(b=>b.Id==book.Id);
            bookDetail.GenreId.Should().Be(book.GenreId);
            bookDetail.PageCount.Should().Be(book.PageCount);
            bookDetail.PublishDate.Should().Be(book.PublishDate);
            bookDetail.Title.Should().Be(book.Title);
            bookDetail.AuthorId.Should().Be(book.AuthorId);
        }
    }
}