using System;
using FluentValidation;

namespace WebApi.Application.AuthorsOperations.Command.CreateAuthor
{
    public class CreateAuthorValidator:AbstractValidator<CreateAuthor>
    {
        public CreateAuthorValidator()
        {
            RuleFor(Au=>Au.newAuthorModel.Name).NotEmpty().MinimumLength(4);
            RuleFor(Au=>Au.newAuthorModel.Surname).NotEmpty().MinimumLength(2);
            RuleFor(Au=>Au.newAuthorModel.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            
        }

    }
}