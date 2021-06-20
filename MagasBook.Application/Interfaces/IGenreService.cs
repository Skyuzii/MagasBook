using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Genre;

namespace MagasBook.Application.Interfaces
{
    public interface IGenreService
    {
        Task<GenreDto> GetAsync(int id);
        Task<GenreDto> CreateAsync(GenreDto genreDto);
        Task<List<GenreDto>> GetAllAsync();
    }
}