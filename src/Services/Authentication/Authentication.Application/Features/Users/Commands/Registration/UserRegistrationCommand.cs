using Authentication.Application.Contracts.Users;
using MediatR;

namespace Authentication.Application.Features.Users.Commands.Registration
{
    public class UserRegistrationCommand : IRequest<UserResponse>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
