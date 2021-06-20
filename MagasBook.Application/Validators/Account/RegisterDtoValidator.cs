using FluentValidation;
using MagasBook.Application.Dto.Account;

namespace MagasBook.Application.Validators.Account
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Имя пользователя не может быть пустым");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Почтовый адрес не может быть пустым")
                .EmailAddress()
                .WithMessage("ыВведите корректный почтовый адрес");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Поле не может быть пустым")
                .MinimumLength(8)
                .WithMessage("Пароль должен быть больше 8 символов")
                .MaximumLength(30)
                .WithMessage("Слишком длинный пароль")
                .Equal(x => x.PasswordConfirm)
                .WithMessage("Пароли не совпадают");
            
            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}