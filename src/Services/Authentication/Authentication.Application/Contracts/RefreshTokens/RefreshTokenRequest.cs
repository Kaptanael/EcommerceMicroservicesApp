namespace Authentication.Application.Contracts.RefreshTokens
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string RefreshToken { get; set; }
    }
}
