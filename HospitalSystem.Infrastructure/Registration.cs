using HospitalSystem.Application.Interfaces.Emails;
using HospitalSystem.Application.Interfaces.RedisCache;
using HospitalSystem.Application.Interfaces.Tokens;
using HospitalSystem.Infrastructure.Emails;
using HospitalSystem.Infrastructure.RedisCache;
using HospitalSystem.Infrastructure.ServiceDiscovery;
using HospitalSystem.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Net.Mail;
using System.Text;

namespace HospitalSystem.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ITokenBlacklistService, TokenBlacklistService>();

            services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));
            services.AddTransient<IRedisCacheService, RedisCacheService>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero 
                };

            });

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer
                .Connect(configuration["RedisCacheSettings:ConnectionString"]));

            services.ConfigureConsul(configuration);

        }
    }
}
