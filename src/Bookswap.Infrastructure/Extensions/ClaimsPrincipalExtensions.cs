using System.Security.Claims;

namespace Bookswap.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string CurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
