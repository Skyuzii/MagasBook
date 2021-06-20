using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Genre;
using MagasBook.Application.Interfaces;
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
            if (id == 0)
            {
                return NotFound();
            }

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
        public async Task<ActionResult> Create([FromBody] GenreDto genreDto)
        {
            var genreCreated = await _genreService.CreateAsync(genreDto);
            return CreatedAtAction(nameof(Get), new {id = genreDto.Id}, genreCreated);
        }
    }
}