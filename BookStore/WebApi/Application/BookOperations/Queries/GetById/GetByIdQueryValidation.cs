using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetByIdQuery
{
    public class GetByIdQueryValidation:AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidation()
        {
            RuleFor(get=>get.Id).GreaterThan(0);
        }

    }
}