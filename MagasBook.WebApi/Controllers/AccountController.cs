using System.Threading.Tasks;
using MagasBook.Application.Dto.Account;
using MagasBook.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _authorizationService.RegisterAsync(registerDto);
            return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {
            var tokenDto = await _authorizationService.LoginAsync(loginDto);
            return Ok(tokenDto);
        }
    }
}