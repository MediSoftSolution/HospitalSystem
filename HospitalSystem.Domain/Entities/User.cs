using Microsoft.AspNetCore.Identity;

namespace HospitalSystem.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Fullname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}