using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Queries.GetAuthorDetails;
using WebApi.Application.GerneOperation.Queries.GetGenreDetails;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Queries.GetGenreDetailsTest
{
    public class  GetAuthorDetailsTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public GetAuthorDetailsTests(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInputAuthorId_NotFound_ShouldBeReturn_Exception()
        {
            //arrange
            var author = new Author{ Name = "WhenInputAuthorId_NotFound_ShouldBeReturn_Exception" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-10)};
            context.Authors.Add(author);
            context.SaveChanges();
            context.Authors.Remove(author);
            context.SaveChanges();

            GetAuthorDetails command = new GetAuthorDetails(context,mapper);
            command.Id=author.Id;
            // Act and Assert
            FluentActions.Invoking(()=>command.Handle())
            .Should().Throw<InvalidOperationException>();

        }
        [Fact]
        public void WhenValidInputAuthorId_GetDetails_ShouldEqualDatas()
        {
            //arrange
            var author = new Author{ Name = "WhenValidInputAuthorId_GetDetails_ShouldEqualDatas" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-10)};
            context.Authors.Add(author);
            context.SaveChanges();

            GetAuthorDetails command = new GetAuthorDetails(context,mapper);
            command.Id=author.Id;
            // Act and Assert
            FluentActions.Invoking(()=>command.Handle());
            var authorDetail = context.Authors.SingleOrDefault(A=>A.Id==command.Id);
            authorDetail.Name.Should().Be(author.Name);
            authorDetail.Surname.Should().Be(author.Surname);
            authorDetail.DateOfBirth.Should().Be(author.DateOfBirth);
        }
    }
}