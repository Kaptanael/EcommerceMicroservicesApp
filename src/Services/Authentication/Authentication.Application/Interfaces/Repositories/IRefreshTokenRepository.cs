using Membership.Domain.Entities;

namespace Membership.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository 
    {
        Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        RefreshToken UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
