using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class WorkingTime : EntityBase
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}