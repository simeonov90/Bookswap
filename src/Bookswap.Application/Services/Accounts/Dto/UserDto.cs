using System.ComponentModel.DataAnnotations;

namespace Bookswap.Application.Services.Accounts.Dto
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password does not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
