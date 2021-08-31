using System;
using FluentValidation;


namespace WebApi.Application.AuthorsOperations.Command.UpdateAuthor
{
    public class UpdateAuthorValidator:AbstractValidator<UpdateAuthor>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(Aut=>Aut.Id).NotEmpty().GreaterThan(0);
            RuleFor(Aut=>Aut.updatedAuthor.Name).NotEmpty().MinimumLength(4);
            RuleFor(Aut=>Aut.updatedAuthor.Surname).NotEmpty().MinimumLength(2);
            RuleFor(Aut=>Aut.updatedAuthor.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
        }

    }
}
