using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Command.UpdateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.AuthorsOperations.Command.UpdateAuthor.UpdateAuthor;

namespace Application.BookOperations.Commands.UpdateBookCommandTest
{
    public class UpdateAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidUpdatedDataInput_IsItRecorded()
        {
            //arrange
            UpdateAuthor Command = new UpdateAuthor(context);
            var author = new Author { Name = "NoUpdated" , Surname = "surname" , DateOfBirth = DateTime.Now.Date.AddYears(-30)};
            context.Authors.Add(author);
            var updatedAuthor = new UpdatedAuthorModel { Name = "YesUpdated" , Surname = "UPDsurname" , DateOfBirth = DateTime.Now.Date.AddYears(-10)};
            Command.Id = author.Id;
            Command.updatedAuthor = updatedAuthor;
            context.SaveChanges();
            // act
            FluentActions.Invoking(() => Command.Handle()).Invoke();
            //assert 
            var GetUpdatedAuthor = context.Authors.SingleOrDefault(b=>b.Id==author.Id);
            GetUpdatedAuthor.Name.Should().Be(updatedAuthor.Name);
            GetUpdatedAuthor.Surname.Should().Be(updatedAuthor.Surname);
             GetUpdatedAuthor.DateOfBirth.Should().Be(updatedAuthor.DateOfBirth);

        }
    
    }
}