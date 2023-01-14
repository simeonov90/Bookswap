using AutoMapper;
using Bookswap.Application.Services.Accounts.Dto;
using Bookswap.Domain.BookswapRoles;
using Bookswap.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bookswap.Application.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly UserManager<BookswapUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AccountService> logger;
        private readonly IConfiguration configuration;

        private const string loginProvider = "BookswapAPI";
        private const string refreshToken = "RefreshToken";

        public AccountService(
            IMapper mapper,
            UserManager<BookswapUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountService> logger,
            IConfiguration configuration
            )
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.configuration = configuration;
        }

        #region JWT
        private async Task<string> GenerateToken(BookswapUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await userManager.GetClaimsAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken
                (
                    issuer: configuration["JwtSettings:Issuer"],
                    audience: configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> CreateRefreshToken(BookswapUser user)
        {
            await userManager.RemoveAuthenticationTokenAsync(user, loginProvider, refreshToken);
            var newRefreshToken = await userManager.GenerateUserTokenAsync(user, loginProvider, refreshToken);
            await userManager.SetAuthenticationTokenAsync(user, loginProvider, refreshToken, newRefreshToken);

            return newRefreshToken;
        }

        public async Task<RequestTokenDto> VerifyRefreshToken(RequestTokenDto requestTokenDto)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(requestTokenDto.Token);
            var userName = tokenContent.Claims.ToList().FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var user = await userManager.FindByNameAsync(userName);

            if (user is null || user.Id != requestTokenDto.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await userManager.VerifyUserTokenAsync(user, loginProvider, refreshToken, requestTokenDto.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken(user);
                return new RequestTokenDto()
                {
                    Token = token,
                    UserId = user.Id,
                    RefreshToken = await CreateRefreshToken(user)
                };
            }

            await userManager.UpdateSecurityStampAsync(user);
            return null;
        }

        #endregion

        public async Task<ResponseTokenDto> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            var isValidUser = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user is null || isValidUser is false) return null;

            var token = await GenerateToken(user);
            return new ResponseTokenDto
            {
                Token = token,
                UserId = user.Id,
                RefreshToken = await CreateRefreshToken(user)
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(UserDto createBookswapUserDto)
        {
            var user = mapper.Map<BookswapUser>(createBookswapUserDto);
            var createdUser = await userManager.CreateAsync(user, createBookswapUserDto.Password);

            if (createdUser.Succeeded)
            {
                var isRoleExist = await roleManager.RoleExistsAsync(BookswapRole.user);
                if (!isRoleExist) await roleManager.CreateAsync(new IdentityRole { Name = BookswapRole.user });

                await userManager.AddToRoleAsync(user, BookswapRole.user);
            }

            return createdUser.Errors;
        }
    }
}
