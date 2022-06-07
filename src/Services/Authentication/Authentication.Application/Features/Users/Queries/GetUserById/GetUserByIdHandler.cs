using Authentication.Application.Contracts.Users;
using Mapster;
using MediatR;
using Membership.Application.Interfaces.Repositories;

namespace Authentication.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userFromRepo = await _unitOfWork.Users.GetAsync(request.UserId, cancellationToken);
            return userFromRepo.Adapt<UserResponse>();
        }
    }
}
