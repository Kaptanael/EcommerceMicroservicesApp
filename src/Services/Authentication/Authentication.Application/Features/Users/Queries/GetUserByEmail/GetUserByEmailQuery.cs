using Authentication.Application.Contracts.Users;
using MediatR;

namespace Authentication.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery: IRequest<UserResponse>
    {
        public string Email { get; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
