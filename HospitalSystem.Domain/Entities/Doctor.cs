using HospitalSystem.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystem.Domain.Entities;

public class Doctor : EntityBase
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Fullname { get; set; }
    public string? About { get; set; }
    public Dictionary<DayOfWeek, WorkingTime> WorkingTimes { get; set; }
    public decimal ConsultingFee { get; set; }

    public ICollection<Appointment> Appointments { get; set; } 
    
    public int PhotoId { get; set; }

    public Photo Photo { get; set; }

    public int SpecialtyId { get; set; }

    public Specialty Specialty { get; set; }

    public int OfficeId { get; set; }

    public Office WorkingOffice { get; set; }

    public Doctor()
    {
        Appointments = new HashSet<Appointment>();
        WorkingTimes = new Dictionary<DayOfWeek, WorkingTime>();  
    }
}
