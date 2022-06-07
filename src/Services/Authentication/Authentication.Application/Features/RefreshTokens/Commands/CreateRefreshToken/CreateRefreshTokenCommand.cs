using Authentication.Application.Contracts.RefreshTokens;
using MediatR;

namespace Authentication.Application.Features.RefreshTokens.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenCommand : IRequest<RefreshTokenResponse>
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
