using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;


namespace Application.BookOperations.Commands.UpdateBookCommandTest
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
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
            UpdateBook command = new UpdateBook(null);
            command.model = new UpdatedBookModel()
            {
                Title = title,
                AuthorId = AuthorId ,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = GenreId
            };
            //act
            UpdateBookValidation validator = new UpdateBookValidation();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturunError()
        {
            UpdateBook command = new UpdateBook(null);
            command.model = new UpdatedBookModel()
            {
                Title = "Lord Of The Rigs",
                AuthorId = 2 ,
                PageCount = 200,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            //act
            UpdateBookValidation validator = new UpdateBookValidation();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateBook command = new UpdateBook(null);
            command.model = new UpdatedBookModel()
            {
                Title = "Lord Of The Rigs",
                AuthorId = 2 ,
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };
            //act
            UpdateBookValidation validator = new UpdateBookValidation();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);

        }
    }
}