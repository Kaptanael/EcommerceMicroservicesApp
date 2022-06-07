using Authentication.Application.Contracts.Users;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;
using Membership.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Features.Users.Commands.Registration
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, UserResponse>
    {

        private readonly IUnitOfWork _unitOfWork;                
        private readonly ILogger<UserRegistrationCommandHandler> _logger;

        public UserRegistrationCommandHandler(IUnitOfWork unitOfWork, ILogger<UserRegistrationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;           
            _logger = logger;
        }

        public async Task<UserResponse> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = new User { UserName = request.UserName, Email = request.Email, PasswordHash = request.PasswordHash, PasswordSalt = request.PasswordSalt };

            var createdUser = await _unitOfWork.Users.AddAsync(userToCreate);

            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"User {createdUser.Id} is successfully created.");

            return userToCreate.Adapt<UserResponse>();
        }
    }
}
