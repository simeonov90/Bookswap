using Bookswap.Application.Services.Accounts.Dto;
using Bookswap.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Bookswap.Application.Services.Accounts
{
    public interface IAccountService
    {
        Task<IEnumerable<IdentityError>> Register(UserDto createBookswapUserDto);
        Task<ResponseTokenDto> Login(LoginDto loginDto);
        Task<string> CreateRefreshToken(BookswapUser user);
        Task<RequestTokenDto> VerifyRefreshToken(RequestTokenDto responseTokenDto);
    }
}
