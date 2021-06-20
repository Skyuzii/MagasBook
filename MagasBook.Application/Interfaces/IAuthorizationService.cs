using System.Threading.Tasks;
using MagasBook.Application.Dto.Account;

namespace MagasBook.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task RegisterAsync(RegisterDto registerDto);

        Task<TokenDto> LoginAsync(LoginDto loginDto);
    }
}