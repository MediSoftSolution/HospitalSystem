using HospitalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Interfaces.Tokens
{
    public interface ITokenBlacklistService
    {
        Task AddToBlacklist(string token, TimeSpan expiry);
        Task<bool> IsTokenRevoked(string token);
    }
}
