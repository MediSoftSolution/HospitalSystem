using HospitalSystem.Application.Interfaces.Tokens;
using Microsoft.AspNetCore.Http;

public class TokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenBlacklistService _tokenBlacklistService;

    public TokenBlacklistMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
    {
        _next = next;
        _tokenBlacklistService = tokenBlacklistService;
    }

    public async Task Invoke(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token) && await _tokenBlacklistService.IsTokenRevoked(token))
        {
           throw new UnauthorizedAccessException("Access Token is revoked.");
        }

        await _next(context);
    }
}
