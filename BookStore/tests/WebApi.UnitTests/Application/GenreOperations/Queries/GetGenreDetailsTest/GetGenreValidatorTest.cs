using FluentAssertions;
using TestSetup;
using WebApi.Application.GerneOperation.Queries.GetGenreDetails;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreDetailsTest
{
    public class GetGenreDetailsValitatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        // Valid Data [InlineData(1)]
        public void whenGerneIdInput_NotVaild_ShouldBeError(int Id)
        {
            //arragence 
            GetGenreDetails command = new GetGenreDetails(null,null);
            command.GenreId = Id;
            GetGenreDetailsValidator validator = new GetGenreDetailsValidator();
            var resutl = validator.Validate(command);
            resutl.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
         public void whenGerneIdInput_Vaild_ShouldBeSucces(int Id)
        {
            //arragence 
            GetGenreDetails command = new GetGenreDetails(null,null);
            command.GenreId = Id;
            GetGenreDetailsValidator validator = new GetGenreDetailsValidator();
            var resutl = validator.Validate(command);
            resutl.Errors.Count.Should().Equals(0);
        }
    }
    
}