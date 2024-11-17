using HospitalSystem.Application.Interfaces.RedisCache;
using MediatR;

namespace HospitalSystem.Application.Features.Auth.Commands.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>, ICacheableQuery
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string CacheKey => "Login";

        public double CacheTime => 60;
    }
}
