using FluentValidation;

namespace WebApi.Application.GerneOperation.Commands.CreateGenre
{
    public class CreateGenreValidator:AbstractValidator<CreateGenre>
    {
        public CreateGenreValidator()
        {
            RuleFor(genre=>genre.model.Name).NotEmpty().MinimumLength(4);
        }
    }
}