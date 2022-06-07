using Authentication.Application.Contracts.Users;
using MediatR;

namespace Authentication.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserPasswordByEmailQuery: IRequest<UserPasswordResponse>
    {
        public string Email { get; }

        public GetUserPasswordByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
