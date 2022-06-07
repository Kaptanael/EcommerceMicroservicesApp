using Authentication.Application.Contracts.Users;
using MediatR;

namespace Authentication.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public string UserId { get; }

        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
