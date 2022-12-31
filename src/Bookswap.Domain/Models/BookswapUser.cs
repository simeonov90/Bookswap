using Microsoft.AspNetCore.Identity;

namespace Bookswap.Domain.Models
{
    public class BookswapUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
