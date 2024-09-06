using HealthcareManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementAPI.Data;

public class HealthCareDbContext : DbContext
{
    public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Patient>().ToTable("Patients");
        modelBuilder.Entity<Doctor>().ToTable("Doctors");
        modelBuilder.Entity<Area>().ToTable("Areas");
        modelBuilder.Entity<Specialization>().ToTable("Specializations");
        modelBuilder.Entity<Cabinet>().ToTable("Cabinets");
    }
}