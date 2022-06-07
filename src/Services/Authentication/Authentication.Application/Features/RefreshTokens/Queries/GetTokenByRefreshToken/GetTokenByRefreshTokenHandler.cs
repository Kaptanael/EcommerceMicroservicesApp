using Authentication.Application.Contracts.RefreshTokens;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;

namespace Authentication.Application.Features.RefreshTokens.Queries.GetTokenByRefreshToken
{
    public class GetTokenByRefreshTokenHandler: IRequestHandler<GetTokenByRefreshTokenQuery, RefreshTokenResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTokenByRefreshTokenHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RefreshTokenResponse> Handle(GetTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var tokenFromRepo = await _unitOfWork.RefreshTokens.GetTokenByRefreshTokenAsync(request.RefreshToken, cancellationToken);
            return tokenFromRepo.Adapt<RefreshTokenResponse>();
        }
    }
}
