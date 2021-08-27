using System;
using FluentValidation;
namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookValidation:AbstractValidator<UpdateBook>
    {
        public UpdateBookValidation()
        {
            RuleFor(Update=>Update.Id).GreaterThan(0);
            RuleFor(Update=>Update.model.GenreId).GreaterThan(0);
            RuleFor(Update=>Update.model.PageCount).GreaterThan(0);
            RuleFor(Update=>Update.model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(Update=>Update.model.Title).NotEmpty().MinimumLength(4);
        }


    }
}