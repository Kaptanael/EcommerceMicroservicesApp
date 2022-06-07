using Authentication.Application.Contracts.Users;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Features.Users.Commands.Login
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;           
        private readonly ILogger<UserLoginCommandHandler> _logger;

        public UserLoginCommandHandler(IUnitOfWork unitOfWork, ILogger<UserLoginCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;                    
            _logger = logger;
        }
        public async Task<UserResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var userFromRepo = await _unitOfWork.Users.FindByEmailAsync(request.Email, cancellationToken);

            if (userFromRepo == null) 
            {
                return null;
            }

            _logger.LogInformation($"User {userFromRepo.Id} is successfully logged in.");

            return userFromRepo.Adapt<UserResponse>();
        }
    }
}
