using System.Reflection;
using HospitalSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Persistence.Context;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<WorkingTime> WorkingTimes { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<TestNameAndResultEntry> TestNameAndResultEntries { get; set; }
    public DbSet<TestTemplate> TestTemplates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }
}