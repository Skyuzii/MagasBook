using System.Threading.Tasks;
using MagasBook.Application.Common.Dto.Account;

namespace MagasBook.Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        Task RegisterAsync(RegisterDto registerDto);

        Task<TokenDto> LoginAsync(LoginDto loginDto);
    }
}