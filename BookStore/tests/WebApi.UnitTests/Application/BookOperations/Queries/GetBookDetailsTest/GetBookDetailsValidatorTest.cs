using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetByIdQuery;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetailsTest
{
    public class GetGenreDetailsValitatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        // Valid Data [InlineData(1)]
        public void whenBookIdInput_NotVaild_ShouldBeError(int Id)
        {
            //arragence 
            GetByIdQuery command = new GetByIdQuery(null,null);
            command.Id = Id;
            GetByIdQueryValidation validator = new GetByIdQueryValidation();
            var resutl = validator.Validate(command);
            resutl.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        // Valid Data [InlineData(1)]
        public void whenBookIdInput_Vaild_ShouldBeSucces(int Id)
        {
            //arragence 
            GetByIdQuery command = new GetByIdQuery(null,null);
            command.Id = Id;
            GetByIdQueryValidation validator = new GetByIdQueryValidation();
            var resutl = validator.Validate(command);
            resutl.Errors.Count.Should().Equals(0);
        }
    }
    
}