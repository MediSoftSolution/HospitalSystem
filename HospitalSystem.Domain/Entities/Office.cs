using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Office : EntityBase
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Tel { get; set; }
    public ICollection<WorkingTime> WorkingTimes { get; set; }
    public ICollection<Photo> Photos { get; set; }

    public Office()
    {
        WorkingTimes = new HashSet<WorkingTime>();
        Photos = new HashSet<Photo>();
    }
}