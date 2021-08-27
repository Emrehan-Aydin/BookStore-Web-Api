using FluentValidation;

namespace WebApi.BookOperations.GetByIdQuery
{
    public class GetByIdQueryValidation:AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidation()
        {
            RuleFor(get=>get.Id).GreaterThan(0);
        }

    }
}