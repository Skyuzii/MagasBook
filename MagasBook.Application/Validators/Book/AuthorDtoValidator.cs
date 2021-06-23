using FluentValidation;
using MagasBook.Application.Dto.Book;

namespace MagasBook.Application.Validators.Book
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не может быть пустым");

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Описание не может быть пустым");
        }
    }
}