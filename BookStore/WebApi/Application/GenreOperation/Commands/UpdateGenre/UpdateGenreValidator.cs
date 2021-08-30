using System;
using System.Linq;
using FluentValidation;
using WebApi.DbOperations;

namespace WebApi.Application.GerneOperation.Commands.UpdateGenre
{
    public class UpdateGenreValidator:AbstractValidator<UpdateGenre>
    {
        public UpdateGenreValidator()
        {   
            RuleFor(genre=>genre.model.Name).MinimumLength(4).When(genre=>genre.model.Name != string.Empty);
            
        }

    }
       
}