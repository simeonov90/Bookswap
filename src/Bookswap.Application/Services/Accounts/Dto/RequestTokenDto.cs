namespace Bookswap.Application.Services.Accounts.Dto
{
    public class RequestTokenDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
