using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Commands.DeleteGenre;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenreCommandTest
{
    public class DeleteGenreCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)] validInput
        public void WhenGenreIdNotGrandThanZeroGiven_ShouldBeReturn(int GenreId)
        {
            //arrange 
            DeleteGenre command = new DeleteGenre(null);
            command.GenreId = GenreId;
            DeleteGenreValidator Validator = new DeleteGenreValidator();
            //act
            var result =  Validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
    
}