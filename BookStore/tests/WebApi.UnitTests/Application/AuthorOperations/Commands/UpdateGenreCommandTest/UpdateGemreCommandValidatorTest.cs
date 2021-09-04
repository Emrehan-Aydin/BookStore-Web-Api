using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Command.UpdateAuthor;
using WebApi.Application.GerneOperation.Commands.UpdateGenre;
using Xunit;
using static WebApi.Application.AuthorsOperations.Command.UpdateAuthor.UpdateAuthor;

namespace Application.AuthorOperations.Commands.UpdateAuthorCommandTest
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("AAAA","AA")]
        [InlineData("AAAAA"," ")]
        [InlineData(" ","AA")]
        [InlineData(" "," ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            // arrange
            UpdateAuthor command = new UpdateAuthor(null);
            command.updatedAuthor = new UpdatedAuthorModel{ 
                
                Name = name , 
                Surname = surname , 
                
                };
            //act
            UpdateAuthorValidator validator = new UpdateAuthorValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }       
        [Fact]
        public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateAuthor command = new UpdateAuthor(null);
            command.updatedAuthor = new UpdatedAuthorModel{ 
                Name = "WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors" , 
                Surname = "surname" , 
                
                };
            //act
            UpdateAuthorValidator validator = new UpdateAuthorValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);

        }
    }
}