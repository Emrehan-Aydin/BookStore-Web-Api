using FluentValidation;

namespace WebApi.BookOperations.RemoveBook
{
    public class RemoveBookValidation:AbstractValidator<RemoveBook>
    {
        public RemoveBookValidation()
        {
            RuleFor(B=>B.id).GreaterThan(0);
        }
    }
}