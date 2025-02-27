using HospitalSystem.Application.Interfaces.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Infrastructure.Tokens
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly IDatabase _redisDb;

        public TokenBlacklistService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task AddToBlacklist(string token, TimeSpan expiry)
        {
            await _redisDb.StringSetAsync(token, "revoked", expiry);
        }

        public async Task<bool> IsTokenRevoked(string token)
        {
            return await _redisDb.KeyExistsAsync(token);
        }
    }
}
