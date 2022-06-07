using Authentication.Application.Contracts.Users;
using Authentication.Application.Responses;

namespace Authentication.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> UserRegistration(UserRegistrationRequest request);  

        Task<AuthenticationResult> Login(UserLoginRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

    }
}
