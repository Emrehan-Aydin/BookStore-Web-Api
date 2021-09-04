using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBookCommand;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBookCommand.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBookCommandTest
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("lord of the ring",0,0,0)]
        [InlineData("lord of the ring",1,0,0)]
        [InlineData("lord of the ring",1,1,0)]
        [InlineData("lord of the ring",0,1,1)]
        [InlineData("lord of the ring",0,0,1)]
        [InlineData("lord of the ring",1,0,1)]
        [InlineData("lord of the ring",0,1,0)]
        [InlineData("Lor",1,1,1)]
        [InlineData("Lor",0,0,0)]
        [InlineData("Lor",1,0,0)]
        [InlineData("Lor",1,1,0)]
        [InlineData("Lor",0,1,1)]
        [InlineData("Lor",0,0,1)]
        [InlineData("Lor",1,0,1)]
        [InlineData("Lor",0,1,0)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int PageCount , int GenreId, int AuthorId )
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                AuthorId = AuthorId ,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = GenreId
            };
            //act
            CreateBookCOmmandValidator validator = new CreateBookCOmmandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturunError()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rigs",
                AuthorId = 2 ,
                PageCount = 200,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            //act
            CreateBookCOmmandValidator validator = new CreateBookCOmmandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rigs",
                AuthorId = 2 ,
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };
            //act
            CreateBookCOmmandValidator validator = new CreateBookCOmmandValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);

        }
    }
}