using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Specialty : EntityBase
{
    public string Name { get; set;} = string.Empty;
    public List<Doctor> Doctors { get; set; }
    public int PhotoId { get; set; }
    public Photo Photo { get; set; }

    public Specialty()
    {
        Doctors = new List<Doctor>();
    }
}