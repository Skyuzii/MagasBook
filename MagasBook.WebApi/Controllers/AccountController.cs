using System.Threading.Tasks;
using FluentValidation;
using MagasBook.Application.Common.Dto;
using MagasBook.Application.Common.Dto.Account;
using MagasBook.Application.Common.Validators.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = MagasBook.Application.Common.Interfaces.IAuthorizationService;

namespace MagasBook.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;

        public AccountController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        
        [HttpPost]
        public async Task<OperationResultDto> Register(RegisterDto registerDto, [FromServices] RegisterDtoValidator registerDtoValidator)
        {
            await registerDtoValidator.ValidateAndThrowAsync(registerDto);
            await _authorizationService.RegisterAsync(registerDto);

            return Success("Пользователь успешно создан", StatusCodes.Status201Created);
        }
        
        [HttpPost]
        public async Task<OperationResultDto<TokenDto>> Login(LoginDto loginDto, [FromServices] LoginDtoValidator loginDtoValidator)
        {
            await loginDtoValidator.ValidateAndThrowAsync(loginDto);
            var tokenDto = await _authorizationService.LoginAsync(loginDto);

            return Success(tokenDto, StatusCodes.Status201Created);
        }
    }
}