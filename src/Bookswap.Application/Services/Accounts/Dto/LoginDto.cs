using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Accounts.Dto
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
