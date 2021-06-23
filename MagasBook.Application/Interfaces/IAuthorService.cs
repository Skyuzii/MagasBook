using System.Collections.Generic;
using System.Threading.Tasks;
using MagasBook.Application.Dto.Book;

namespace MagasBook.Application.Interfaces
{
    public interface IAuthorService
    {
        Task DeleteAsync(int id);
        Task<AuthorDto> GetAsync(int id);
        Task<AuthorDto> CreateAsync(AuthorDto authorDto);
        Task EditAsync(AuthorDto authorDto);
        Task<List<AuthorDto>> GetAllAsync();
    }
}