using AutoMapper;
using Bookswap.Application.Services.Accounts.Dto;
using Bookswap.Domain.BookswapRoles;
using Bookswap.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AccountService(
            IMapper mapper, 
            UserManager<BookswapUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountService> logger
            )
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user is null) return false;

            var isValidCredentials = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (isValidCredentials is false) return false;
            
            return isValidCredentials;
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
