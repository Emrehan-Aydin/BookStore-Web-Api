using FluentValidation;

namespace WebApi.Application.GerneOperation.Commands.DeleteGenre
{
    public class DeleteGenreValidator:AbstractValidator<DeleteGenre>
    {
        public DeleteGenreValidator()
        {
            RuleFor(Gerne=>Gerne.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}