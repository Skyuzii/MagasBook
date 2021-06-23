using System.Threading.Tasks;
using MagasBook.Application.Dto.Account;
using MagasBook.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MagasBook.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _authenticationService.RegisterAsync(registerDto);
            return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {
            var tokenDto = await _authenticationService.LoginAsync(loginDto);
            return tokenDto;
        }
    }
}