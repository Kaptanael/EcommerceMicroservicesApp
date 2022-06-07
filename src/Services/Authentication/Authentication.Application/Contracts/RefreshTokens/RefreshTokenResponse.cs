namespace Authentication.Application.Contracts.RefreshTokens
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public Guid UserId { get; set; }        
    }
}
