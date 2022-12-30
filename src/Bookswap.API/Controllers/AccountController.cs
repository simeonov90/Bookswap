using Bookswap.Application.Services.Accounts;
using Bookswap.Application.Services.Accounts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService bookswapUserService;

        public AccountController(IAccountService bookswapUserService)
        {
            this.bookswapUserService = bookswapUserService;
        }

        // POST: api/Account/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody]UserDto createBookswapUserDto)
        {
            var erros = await bookswapUserService.Register(createBookswapUserDto);

            if (erros.Any())
            {
                foreach (var error in erros)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest();
            }

            return Ok(createBookswapUserDto);
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var isValidUser = await bookswapUserService.Login(loginDto);
            if (isValidUser is false) return Unauthorized();

            return Ok();
        }
    }
}
