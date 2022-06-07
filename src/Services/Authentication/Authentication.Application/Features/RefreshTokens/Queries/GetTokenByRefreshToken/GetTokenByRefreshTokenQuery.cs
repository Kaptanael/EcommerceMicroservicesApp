using Authentication.Application.Contracts.RefreshTokens;
using MediatR;

namespace Authentication.Application.Features.RefreshTokens.Queries.GetTokenByRefreshToken
{
    public class GetTokenByRefreshTokenQuery : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; }

        public GetTokenByRefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
