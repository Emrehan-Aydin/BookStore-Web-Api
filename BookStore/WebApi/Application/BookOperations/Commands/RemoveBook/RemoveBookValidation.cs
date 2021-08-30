using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.RemoveBook
{
    public class RemoveBookValidation:AbstractValidator<RemoveBook>
    {
        public RemoveBookValidation()
        {
            RuleFor(B=>B.id).GreaterThan(0);
        }
    }
}