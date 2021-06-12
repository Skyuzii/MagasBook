using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagasBook.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        [HttpGet]
        public IActionResult Status()
        {
            return BadRequest("Ужас");
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(5);
        }
    }
}