using Authentication.Application.Contracts.Users;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;

namespace Authentication.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;        
        public GetUserByEmailHandler(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;            
        }
        public async Task<UserResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var userFromRepo = await _unitOfWork.Users.FindByEmailAsync(request.Email, cancellationToken);
            return userFromRepo.Adapt<UserResponse>();
        }        
    }
}
