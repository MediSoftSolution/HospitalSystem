using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.Tokens;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Auth.Commands.RevokeUserAccessToken
{
    public class RevokeAccessTokenCommandHandler : BaseHandler, IRequestHandler<RevokeAccessTokenCommandRequest, Unit>
    {
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RevokeAccessTokenCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ITokenBlacklistService tokenBlacklistService)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _tokenBlacklistService = tokenBlacklistService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(RevokeAccessTokenCommandRequest request, CancellationToken cancellationToken)
        {
            string? accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new UnauthorizedAccessException("Access token tapılmadı.");
            }

            accessToken = accessToken.Replace("Bearer ", "").Trim();

            TimeSpan expiryTime = TimeSpan.FromMinutes(60);
            await _tokenBlacklistService.AddToBlacklist(accessToken, expiryTime);

            return Unit.Value;
        }
    }
}
