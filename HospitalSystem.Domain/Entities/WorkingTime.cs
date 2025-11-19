using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class WorkingTime : EntityBase
{
    public TimeSpan Start { get; set; } 
    public TimeSpan End { get; set; }
}