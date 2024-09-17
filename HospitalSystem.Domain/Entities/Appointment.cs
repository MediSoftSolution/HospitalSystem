using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Appointment : EntityBase
{
    public Guid PatientId { get; set; }
    public string Message { get; set; }
    
    public DateTime ConsultingDate { get; set; }
    
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}