using FluentValidation;
using MagasBook.Application.Dto.Book;

namespace MagasBook.Application.Validators.Book
{
    public class GenreDtoValidator : AbstractValidator<GenreDto>
    {
        public GenreDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название не может быть пустым");
        }
    }
}