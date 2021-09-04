using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorsOperations.Queries.GetAuthorDetails;
using WebApi.Application.GerneOperation.Queries.GetGenreDetails;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthoDetailsTest
{
    public class GetAuthorDetailsValitatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        // Valid Data [InlineData(1)]
        public void whenAuthorIdInput_NotVaild_ShouldBeError(int Id)
        {
            //arragence 
            GetAuthorDetails command = new GetAuthorDetails(null,null);
            command.Id = Id;
            GetAuthorDetailsValidator validator = new GetAuthorDetailsValidator();
            var resutl = validator.Validate(command);
            resutl.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
         public void whenAuthorIdInput_Vaild_ShouldBeSucces(int Id)
        {
            //arragence 
            GetAuthorDetails command = new GetAuthorDetails(null,null);
            command.Id = Id;
            GetAuthorDetailsValidator validator = new GetAuthorDetailsValidator();
            var resutl = validator.Validate(command);
            resutl.Errors.Count.Should().Equals(0);
        }
    }
    
}