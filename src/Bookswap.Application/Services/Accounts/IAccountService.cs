using Bookswap.Application.Services.Accounts.Dto;
using Microsoft.AspNetCore.Identity;

namespace Bookswap.Application.Services.Accounts
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> Register(UserDto createBookswapUserDto);
        Task<bool> Login(LoginDto loginDto);
    }
}
