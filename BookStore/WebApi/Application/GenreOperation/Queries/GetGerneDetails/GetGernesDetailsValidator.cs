using FluentValidation;

namespace WebApi.Application.GerneOperation.Queries.GetGenreDetails
{
    public class GetGenreDetailsValidator:AbstractValidator<GetGenreDetails>
    {
        public GetGenreDetailsValidator()
        {
            RuleFor(query=>query.GenreId).GreaterThan(0);   
        }
    }
}