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

        private string ExtractTokenFromHeader(HttpContext? context)
        {
            var token = context?.Request.Headers["Authorization"].ToString().Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Missing token.");
            }
            return token;
        }

        private string ExtractNonceFromHeader(HttpContext? context)
        {
            var nonce = context?.Request.Headers["Nonce"].FirstOrDefault();
            if (string.IsNullOrEmpty(nonce) || !Guid.TryParse(nonce, out _))
            {
                throw new UnauthorizedAccessException("Invalid or missing nonce.");
            }
            return nonce;
        }

        private async Task ValidateNonceAsync(string nonce)
        {
            try
            {
                await tokenService.ValidateNonceAsync(nonce);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Invalid or reused nonce.");
            }
        }

        private void ValidateUserClaims(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Invalid user claims.");
            }
        }
    }
}
