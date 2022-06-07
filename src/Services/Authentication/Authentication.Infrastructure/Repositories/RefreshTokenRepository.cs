using Authentication.Infrastructure.Persistence;
using Membership.Application.Interfaces.Repositories;
using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.Repositories
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {
        private readonly MembershipDbContext _context;

        public RefreshTokenRepository(MembershipDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            return refreshToken;
        }        

        public RefreshToken UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);
        }
        
    }
}
