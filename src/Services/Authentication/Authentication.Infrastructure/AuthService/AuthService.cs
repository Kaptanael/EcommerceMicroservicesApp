using Authentication.Application.Contracts.Infrastructure;
using Authentication.Application.Contracts.Users;
using Authentication.Application.Features.RefreshTokens.Commands.UpdateRefreshToken;
using Authentication.Application.Features.RefreshTokens.Queries.GetTokenByRefreshToken;
using Authentication.Application.Features.Users.Commands.Login;
using Authentication.Application.Features.Users.Commands.Registration;
using Authentication.Application.Features.Users.Queries.GetUserByEmail;
using Authentication.Application.Features.Users.Queries.GetUserById;
using Authentication.Application.Interfaces;
using Authentication.Application.Responses;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentication.Infrastructure.AuthService
{
    public class AuthService:IAuthService
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordHasher;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthService(IMediator mediator, ITokenService tokenService, IPasswordService passwordService, TokenValidationParameters tokenValidationParameters)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _passwordHasher = passwordService;
            _tokenValidationParameters = tokenValidationParameters;
        }
        public async Task<AuthenticationResult> UserRegistration(UserRegistrationRequest request)
        {            
            byte[] passwordHash, passwordSalt;

            var userRegistercommand = request.Adapt<UserRegistrationCommand>();

            var query = new GetUserByEmailQuery(userRegistercommand.Email);

            var existingUser = await _mediator.Send(query);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email addess already exist" }
                };
            }

            _passwordHasher.CreatePasswordHash(userRegistercommand.Password, out passwordHash, out passwordSalt);
            userRegistercommand.PasswordHash = passwordHash;
            userRegistercommand.PasswordSalt = passwordSalt;

            var newUser = await _mediator.Send(userRegistercommand);

            var authenticationResponse = await _tokenService.GenerateAuthenticationResultForUserAsync(newUser);            

            return new AuthenticationResult
            {
                Success = authenticationResponse.Success,
                Token = authenticationResponse.Token,
                RefreshToken = authenticationResponse.RefreshToken                
            };

        }

        public async Task<AuthenticationResult> Login(UserLoginRequest request) 
        {
            var userLogincommand = request.Adapt<UserLoginCommand>();

            var userFromRepo = await _mediator.Send(userLogincommand);

            if (userFromRepo == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var query = new GetUserPasswordByEmailQuery(userLogincommand.Email);            

            var userPasswordFromRepo = await _mediator.Send(query);

            if (!_passwordHasher.VerifyPasswordHash(userLogincommand.Password, userPasswordFromRepo.PasswordHash, userPasswordFromRepo.PasswordSalt))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User or password combination is wrong" }
                };
            }

            var authenticationResponse = await _tokenService.GenerateAuthenticationResultForUserAsync(userFromRepo);            

            return new AuthenticationResult
            {
                Success = authenticationResponse.Success,
                Token = authenticationResponse.Token,
                RefreshToken = authenticationResponse.RefreshToken                
            };
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token has not expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;            

            var getTokenByRefreshTokenQuery = new GetTokenByRefreshTokenQuery(refreshToken);

            var storedRefreshToken = await _mediator.Send(getTokenByRefreshTokenQuery);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this jwt" } };
            }

            storedRefreshToken.Used = true;

            var updateRefreshTokenCommand = refreshToken.Adapt<UpdateRefreshTokenCommand>();

            await _mediator.Send(updateRefreshTokenCommand);            

            var getUserByIdQuery = new GetUserByIdQuery(validatedToken.Claims.Single(x => x.Type == "id").Value);

            var userFromRepo = await _mediator.Send(getUserByIdQuery);

            var authenticationResponse = await _tokenService.GenerateAuthenticationResultForUserAsync(userFromRepo);

            return new AuthenticationResult
            {
                Success = authenticationResponse.Success,
                Token = authenticationResponse.Token,
                RefreshToken = authenticationResponse.RefreshToken
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }

        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        }
    }
}
