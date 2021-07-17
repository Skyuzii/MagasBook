using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagasBook.WebApi.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> Get(int id)
        {
            var genreDto = await _genreService.GetAsync(id);
            return genreDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            return genres;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GenreDto>> Create([FromBody] GenreDto genreDto)
        {
            var genreCreated = await _genreService.CreateAsync(genreDto);
            return CreatedAtAction(nameof(Get), new {id = genreDto.Id}, genreCreated);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);
            return NoContent();
        }
    }
}