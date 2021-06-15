using FluentValidation;
using MagasBook.Application.Common.Dto.Account;

namespace MagasBook.Application.Common.Validators.Account
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Поле не может быть пустым");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Поле не может быть пустым");
        }
    }
}