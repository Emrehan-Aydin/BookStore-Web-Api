using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Command.CreateAuthor;
using WebApi.Application.GerneOperation.Commands.CreateGenre;
using Xunit;
using static WebApi.Application.AuthorsOperations.Command.CreateAuthor.CreateAuthor;

namespace Application.AuthorOperations.Commands.CreateAuthorCommandTest
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        // valid Input [InlineData("lord")]
        [InlineData("aaaaa","aa")]
        [InlineData("aaa","aaa")]
        [InlineData(" ","asd")]
        [InlineData("asdfg"," ")]    

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            // arrange
            CreateAuthor command = new CreateAuthor(null,null);
            command.newAuthorModel = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
            };
            //act
            CreateAuthorValidator validator = new CreateAuthorValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
    
        public void WhenvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateAuthor command = new CreateAuthor(null,null);
            command.newAuthorModel = new CreateAuthorModel()
            {
                Name = "aaaa",
                Surname = "ak",
                DateOfBirth = DateTime.Now.Date.AddYears(-10)
            };
            //act
            CreateAuthorValidator validator = new CreateAuthorValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().Equals(0);
        }
        [Fact]
        public void WhenInvalidDateInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateAuthor command = new CreateAuthor(null,null);
            command.newAuthorModel = new CreateAuthorModel()
            {
                Name = "aaa",
                Surname = "a",
                DateOfBirth = DateTime.Now.Date
            };
            //act
            CreateAuthorValidator validator = new CreateAuthorValidator();
            var result = validator.Validate(command);
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}