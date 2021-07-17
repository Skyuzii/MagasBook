using System.Threading.Tasks;
using MagasBook.Application.Dto.Account;

namespace MagasBook.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDto registerDto);

        Task<TokenDto> LoginAsync(LoginDto loginDto);
    }
}