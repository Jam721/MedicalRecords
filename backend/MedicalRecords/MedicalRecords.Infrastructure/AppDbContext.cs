using MedicalRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Disease> Diseases => Set<Disease>();
}