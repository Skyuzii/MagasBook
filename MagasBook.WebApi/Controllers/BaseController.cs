using System;
using Microsoft.AspNetCore.Mvc;

namespace MagasBook.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
    }
}