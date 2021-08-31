using FluentValidation;

namespace WebApi.Application.AuthorsOperations.Command.DeleteAuthor
{
    public class DeleteAuthorValidator:AbstractValidator<DeleteAuthor>
    {
        public DeleteAuthorValidator()
        {
            RuleFor(Aut=>Aut.Id).NotEmpty().GreaterThan(0);
        }
        
    }
}