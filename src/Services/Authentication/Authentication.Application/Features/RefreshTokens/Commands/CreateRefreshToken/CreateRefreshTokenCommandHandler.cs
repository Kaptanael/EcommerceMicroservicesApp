using Authentication.Application.Contracts.RefreshTokens;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;
using Membership.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Features.RefreshTokens.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUnitOfWork _unitOfWork;             
        private readonly ILogger<CreateRefreshTokenCommandHandler> _logger;

        public CreateRefreshTokenCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateRefreshTokenCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;                        
            _logger = logger;
        }
        public async Task<RefreshTokenResponse> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenToCreate = request.Adapt<RefreshToken>();

            var createdRefreshToken = await _unitOfWork.RefreshTokens.CreateRefreshTokenAsync(refreshTokenToCreate, cancellationToken);

            await _unitOfWork.CompleteAsync(cancellationToken);

            _logger.LogInformation($"Refresh token {createdRefreshToken.Token} is successfully created.");

            return createdRefreshToken.Adapt<RefreshTokenResponse>();
        }
    }
}
