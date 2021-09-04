using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBookCommand
{
    public class CreateBookCOmmandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCOmmandValidator(){
            RuleFor(command=>command.Model.GenreId).GreaterThan(0);
            RuleFor(command=>command.Model.PageCount).GreaterThan(0);
            RuleFor(command=>command.Model.AuthorId).GreaterThan(0);
            RuleFor(command=>command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);
        }

    }

}