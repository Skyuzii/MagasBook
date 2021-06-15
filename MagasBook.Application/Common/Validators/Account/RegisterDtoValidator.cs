﻿using FluentValidation;
using MagasBook.Application.Common.Dto.Account;

namespace MagasBook.Application.Common.Validators.Account
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Поле не может быть пустым");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Поле не может быть пустым")
                .EmailAddress()
                .WithMessage("Введите корректный почтовый адрес");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Поле не может быть пустым")
                .MinimumLength(8)
                .WithMessage("Пароль должен быть больше 8 символов")
                .MaximumLength(30)
                .WithMessage("Слишком длинный пароль")
                .NotEqual(x => x.PasswordConfirm)
                .WithMessage("Пароли не совпадают");
            
            RuleFor(x => x.PasswordConfirm)
                .NotEqual(x => x.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}