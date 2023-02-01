using Bookswap.Application.Extensions.ExceptionMessages;
using Bookswap.Application.Services.Accounts;
using Bookswap.Application.Services.Accounts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Bookswap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        // POST: api/Account/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody]UserDto createBookswapUserDto)
        {
            logger.LogInformation($"Registration attempt for {createBookswapUserDto.Email}");
            try
            {
                var errors = await accountService.Register(createBookswapUserDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(LogErrorExcepitonMessage.SomethingWentWrong(nameof(Register), ex.Message)); 
                return Problem(CommonExceptionMessage.SomethingWentWrongContactSupport(nameof(Register)), statusCode: 500);
            }
            
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            logger.LogInformation($"Login attempt for {loginDto.UserName}");
            try
            {
                var responseToken = await accountService.Login(loginDto);
                if (responseToken is null) return Unauthorized();

                return Ok(responseToken);
            }
            catch (Exception ex)
            {
                logger.LogError(LogErrorExcepitonMessage.SomethingWentWrong(nameof(Login), ex.Message));
                return Problem(CommonExceptionMessage.SomethingWentWrongContactSupport(nameof(Login)), statusCode: 500);
            }
        }

        // POST: api/Account/RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] RequestTokenDto requestTokenDto)
        {
            var responseToken = await accountService.VerifyRefreshToken(requestTokenDto);
            if (responseToken is null) return Unauthorized();

            return Ok(responseToken);
        }
    }
}
