using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagasBook.WebApi.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            var genreDto = await _authorService.GetAsync(id);
            return genreDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
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
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}