using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Commands.CreateGenre;
using Xunit;
namespace Application.GenreOperations.Commands.CreateGenreCommandTest
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        // valid Input [InlineData("lord")]
        [InlineData("")]
        [InlineData("l")]
        [InlineData("lo")]
        [InlineData("lor")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            // arrange
            CreateGenre command = new CreateGenre(null,null);
            command.model = new CreateGenreModel()
            {
                Name = name
            };
            //act
            CreateGenreValidator validator = new CreateGenreValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateGenre command = new CreateGenre(null,null);
            command.model = new CreateGenreModel()
            {
                Name = "GenreName"
            };
            //act
            CreateGenreValidator validator = new CreateGenreValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);

        }
    }
}