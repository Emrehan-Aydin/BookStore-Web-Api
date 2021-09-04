using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Command.DeleteAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthorCommandTest
{
    public class DeleteAuthorCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)] validInput
        public void WhenAuthorIdNotGrandThanZeroGiven_ShouldBeReturn(int AuthorId)
        {
            //arrange 
            DeleteAuthor command = new DeleteAuthor(null);
            command.Id = AuthorId;
            DeleteAuthorValidator Validator = new DeleteAuthorValidator();
            //act
            var result =  Validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
    
}