namespace Bookswap.Application.Services.Accounts.Dto
{
    public class ResponseTokenDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
