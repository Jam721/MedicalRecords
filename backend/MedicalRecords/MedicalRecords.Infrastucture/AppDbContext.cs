using MedicalRecords.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastucture;

public class AppDbContext
    (DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PatientEntity> Patients => Set<PatientEntity>();
    public DbSet<DoctorEntity> Doctors => Set<DoctorEntity>();
    public DbSet<DiseaseEntity> Diseases => Set<DiseaseEntity>();
}