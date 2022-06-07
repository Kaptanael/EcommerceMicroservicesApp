using Authentication.Application.Contracts.Users;
using Authentication.Application.Features.RefreshTokens.Commands.CreateRefreshToken;
using Authentication.Application.Interfaces;
using Authentication.Application.Responses;
using Mapster;
using MediatR;
using Membership.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;                
        private readonly IMediator _mediator;

        public TokenService( JwtSettings jwtSettings, IMediator mediator)
        {
            _jwtSettings = jwtSettings;                        
            _mediator = mediator;
        }
        
        public async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(UserResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };            

            var createRefreshTokenCommand = refreshToken.Adapt<CreateRefreshTokenCommand>();
            var createdRefreshToken = await _mediator.Send(createRefreshTokenCommand);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = createdRefreshToken.Token
            };
        }        
    }
}
