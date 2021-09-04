using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.RemoveBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBookCommandTest
{
    public class DeleteBookCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)] validInput
        public void WhenBookIdNotGrandThanZeroGiven_ShouldBeReturn(int BookId)
        {
            //arrange 
            RemoveBook command = new RemoveBook(null);
            command.id = BookId;
            RemoveBookValidation Validator = new RemoveBookValidation();
            //act
            var result =  Validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
    
}