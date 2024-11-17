using HospitalSystem.Application.Interfaces.RedisCache;
using HospitalSystem.Application.Interfaces.Tokens;
using HospitalSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HospitalSystem.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> userManager;
        private readonly TokenSettings tokenSettings;
        private readonly IRedisCacheService redisCacheService;


        public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager, IRedisCacheService redisCacheService)
        {
            tokenSettings = options.Value;
            this.userManager = userManager;
            this.redisCacheService = redisCacheService;
        }
        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {

            var nonce = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("nonce", nonce)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                expires: DateTime.Now.AddMinutes(tokenSettings.TokenValidityInMunitues),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            await userManager.AddClaimsAsync(user, claims);

            await redisCacheService.SetAsync($"nonce:{nonce}", true, DateTime.Now.AddMinutes(60));
            return token;

        }

        public async Task ValidateNonceAsync(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var nonceClaim = principal?.Claims.FirstOrDefault(c => c.Type == "nonce");
            var nonce = nonceClaim?.Value;

            if (string.IsNullOrEmpty(nonce))
            {
                throw new UnauthorizedAccessException("Token does not contain a valid nonce.");
            }

            var isNonceUsed = await redisCacheService.GetAsync<bool>($"nonce:{nonce}");
            if (!isNonceUsed)
            {
                throw new UnauthorizedAccessException("Invalid or reused nonce.");
            }

            await redisCacheService.SetAsync($"nonce:{nonce}", false, DateTime.Now.AddMinutes(60));
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters tokenValidationParamaters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParamaters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Couldn't find the token.");

            return principal;

        }

        

    }
}
