using FluentValidation;

namespace WebApi.Application.AuthorsOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailsValidator:AbstractValidator<GetAuthorDetails>
    {
        public GetAuthorDetailsValidator()
        {
            RuleFor(aut=>aut.Id).NotEmpty().GreaterThan(0);
        }
    }
}