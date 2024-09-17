using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Specialty : EntityBase
{
    public string Name { get; set;}
    public List<Doctor> Doctors { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
    public Photo Photo { get; set; }

    public Specialty()
    {
        Doctors = new List<Doctor>();
    }
}