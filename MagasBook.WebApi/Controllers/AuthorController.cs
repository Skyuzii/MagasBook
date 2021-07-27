using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MagasBook.WebApi.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            _logger.LogWarning("TEST GET");
            var genreDto = await _authorService.GetAsync(id);
            return genreDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            _logger.LogInformation("TEST GETALL");
            var genres = await _authorService.GetAllAsync();
            return genres;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorDto>> Create([FromBody] AuthorDto authorDto)
        {
            var authorCreated = await _authorService.CreateAsync(authorDto);
            return CreatedAtAction(nameof(Get), new {id = authorDto.Id}, authorCreated);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [FromBody] AuthorDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }
            
            await _authorService.EditAsync(authorDto);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogCritical("TEST DELETE");
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}