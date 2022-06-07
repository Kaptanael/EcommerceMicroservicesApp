using Authentication.Application.Contracts.Users;
using MediatR;

namespace Authentication.Application.Features.Users.Commands.Login
{
    public class UserLoginCommand:IRequest<UserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
