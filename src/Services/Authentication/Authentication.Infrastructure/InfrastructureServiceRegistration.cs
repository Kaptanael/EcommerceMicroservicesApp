using Authentication.Application.Contracts.Infrastructure;
using Authentication.Application.Interfaces;
using Authentication.Application.Responses;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Services;
using Membership.Application.Interfaces.Repositories;
using Membership.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MembershipDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.Configure<JwtSettings>(c => configuration.GetSection("JwtSettings"));

            services.AddTransient<IEmailService, EmailService>();


            //var jwtSettings = new JwtSettings();
            //configuration.Bind(nameof(jwtSettings), jwtSettings);
            //services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(new JwtSettings().Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthService, AuthService.AuthService>();

            return services;
        }
    }
}
