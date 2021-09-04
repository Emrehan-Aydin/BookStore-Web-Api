using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GerneOperation.Commands.UpdateGenre;
using Xunit;


namespace Application.GenreOperations.Commands.UpdateGenreCommandTest
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("A")]
        [InlineData("AB")]
        [InlineData("ABC")]
        [InlineData(" ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // arrange
            UpdateGenre command = new UpdateGenre(null);
            command.model = new UpdateGenreModel()
            {
               Name = name
            };
            //act
            UpdateGenreValidator validator = new UpdateGenreValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }       
        [Fact]
        public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateGenre command = new UpdateGenre(null);
            command.model = new UpdateGenreModel()
            {
                Name = "Lord Of The Rigs",
            };
            //act
            UpdateGenreValidator validator = new UpdateGenreValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);

        }
    }
}