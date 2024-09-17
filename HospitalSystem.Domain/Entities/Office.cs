using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Office : EntityBase
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Tel { get; set; }
    public Dictionary<DayOfWeek, WorkingTime> WorkingTimes { get; set; }
    public ICollection<Specialty> Specialties { get; set; }
    public ICollection<Photo> Photos { get; set; }

    public Office()
    {
        WorkingTimes = new Dictionary<DayOfWeek, WorkingTime>();
        Specialties = new HashSet<Specialty>();
        Photos = new HashSet<Photo>();
    }
}