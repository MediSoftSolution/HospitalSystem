using HospitalSystem.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystem.Domain.Entities;

public class User : IdentityUser<Guid>, IEntityBase
{
    public string Fullname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}