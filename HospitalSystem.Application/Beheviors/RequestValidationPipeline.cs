using HospitalSystem.Application.Features.Auth.Commands.Login;
using HospitalSystem.Application.Features.Auth.Commands.Register;
using HospitalSystem.Application.Interfaces.Tokens;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Behaviors
{
    public class AuthorizationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ITokenService tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationPipelineBehavior(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            this.tokenService = tokenService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is LoginCommandRequest || request is RegisterCommandRequest) next();

            //var context = _httpContextAccessor.HttpContext;

            //var token = ExtractTokenFromHeader(context);
            //var principal = tokenService.GetPrincipalFromExpiredToken(token);
            //if (principal == null)
            //{
            //    throw new UnauthorizedAccessException("Invalid token.");
            //}

            //var nonce = ExtractNonceFromHeader(context);
            //await ValidateNonceAsync(nonce);

            //ValidateUserClaims(principal);

            return await next();
        }





        
    }
}
