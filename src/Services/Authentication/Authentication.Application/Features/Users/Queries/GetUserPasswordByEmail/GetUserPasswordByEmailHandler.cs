using Authentication.Application.Contracts.Users;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;

namespace Authentication.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserPasswordByEmailHandler : IRequestHandler<GetUserPasswordByEmailQuery, UserPasswordResponse>
    {
        private readonly IUnitOfWork _unitOfWork;        
        public GetUserPasswordByEmailHandler(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;           
        }
        public async Task<UserPasswordResponse> Handle(GetUserPasswordByEmailQuery request, CancellationToken cancellationToken)
        {
            var userFromRepo = await _unitOfWork.Users.FindByEmailAsync(request.Email, cancellationToken);
            return userFromRepo.Adapt<UserPasswordResponse>();
        }
    }
}
