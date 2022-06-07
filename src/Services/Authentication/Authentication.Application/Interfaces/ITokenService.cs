using Authentication.Application.Contracts.Users;
using Authentication.Application.Responses;

namespace Authentication.Application.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(UserResponse user);
    }
}
