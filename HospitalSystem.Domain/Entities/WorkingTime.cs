using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class WorkingTime : EntityBase
{
    public DayOfWeek Day { get; set; } 
    public TimeSpan Start { get; set; } 
    public TimeSpan End { get; set; } 

    public Guid DoctorId { get; set; } 
    public Doctor Doctor { get; set; }
}