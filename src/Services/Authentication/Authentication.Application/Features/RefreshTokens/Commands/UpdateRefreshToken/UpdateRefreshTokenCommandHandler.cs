using Authentication.Application.Contracts.RefreshTokens;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;
using Membership.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Features.RefreshTokens.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRefreshTokenCommandHandler> _logger;

        public UpdateRefreshTokenCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateRefreshTokenCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<RefreshTokenResponse> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenToUpdate = request.Adapt<RefreshToken>();

            var updatedRefreshToken = _unitOfWork.RefreshTokens.UpdateRefreshTokenAsync(refreshTokenToUpdate);

            await _unitOfWork.CompleteAsync(cancellationToken);

            _logger.LogInformation($"Refresh token {updatedRefreshToken.Token} is successfully updated.");

            return updatedRefreshToken.Adapt<RefreshTokenResponse>();
        }
    }
}
