using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Book;

namespace MagasBook.Application.Interfaces
{
    public interface IGenreService
    {
        Task DeleteAsync(int id);
        Task<GenreDto> GetAsync(int id);
        Task<GenreDto> CreateAsync(GenreDto genreDto);
        Task<List<GenreDto>> GetAllAsync();
    }
}