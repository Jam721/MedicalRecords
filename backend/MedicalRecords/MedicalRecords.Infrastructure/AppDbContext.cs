using MedicalRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Disease> Diseases => Set<Disease>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация связей и индексов
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasOne(p => p.Doctor)
                  .WithMany(d => d.Patients)
                  .HasForeignKey(p => p.DoctorId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(p => p.DoctorId);
            entity.HasIndex(p => new { p.LastName, p.FirstName });
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasIndex(d => d.LicenseNumber).IsUnique();
            entity.HasIndex(d => d.Specialization);
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasIndex(d => d.Name).IsUnique();
        });

        // Seed данные
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Врачи
        var doctors = new[]
        {
            new Doctor { Id = 1, FirstName = "Иван", LastName = "Петров", Specialization = "Кардиолог", LicenseNumber = "LIC001" },
            new Doctor { Id = 2, FirstName = "Мария", LastName = "Иванова", Specialization = "Невролог", LicenseNumber = "LIC002" },
            new Doctor { Id = 3, FirstName = "Алексей", LastName = "Сидоров", Specialization = "Терапевт", LicenseNumber = "LIC003" },
            new Doctor { Id = 4, FirstName = "Ольга", LastName = "Кузнецова", Specialization = "Дерматолог", LicenseNumber = "LIC004" },
            new Doctor { Id = 5, FirstName = "Сергей", LastName = "Васильев", Specialization = "Офтальмолог", LicenseNumber = "LIC005" },
            new Doctor { Id = 6, FirstName = "Екатерина", LastName = "Николаева", Specialization = "Педиатр", LicenseNumber = "LIC006" },
            new Doctor { Id = 7, FirstName = "Дмитрий", LastName = "Орлов", Specialization = "Хирург", LicenseNumber = "LIC007" },
            new Doctor { Id = 8, FirstName = "Анна", LastName = "Захарова", Specialization = "Гинеколог", LicenseNumber = "LIC008" }
        };

        // Заболевания
        var diseases = new[]
        {
            new Disease { Id = 1, Name = "Грипп", Description = "Острое инфекционное заболевание дыхательных путей", Symptoms = "Высокая температура, головная боль, слабость, боль в мышцах" },
            new Disease { Id = 2, Name = "Гипертония", Description = "Повышенное артериальное давление", Symptoms = "Головная боль, головокружение, шум в ушах" },
            new Disease { Id = 3, Name = "Сахарный диабет", Description = "Эндокринное заболевание, связанное с нарушением усвоения глюкозы", Symptoms = "Жажда, частое мочеиспускание, усталость" },
            new Disease { Id = 4, Name = "Астма", Description = "Хроническое воспалительное заболевание дыхательных путей", Symptoms = "Одышка, кашель, хрипы" },
            new Disease { Id = 5, Name = "Артрит", Description = "Воспаление суставов", Symptoms = "Боль в суставах, скованность, припухлость" },
            new Disease { Id = 6, Name = "Гастрит", Description = "Воспаление слизистой оболочки желудка", Symptoms = "Боль в желудке, тошнота, изжога" },
            new Disease { Id = 7, Name = "Мигрень", Description = "Неврологическое заболевание", Symptoms = "Сильная головная боль, тошнота, светочувствительность" },
            new Disease { Id = 8, Name = "Остеохондроз", Description = "Дегенеративное заболевание позвоночника", Symptoms = "Боль в спине, ограничение подвижности" },
            new Disease { Id = 9, Name = "Пневмония", Description = "Воспаление легких", Symptoms = "Кашель, высокая температура, боль в груди" },
            new Disease { Id = 10, Name = "Аллергия", Description = "Повышенная чувствительность иммунной системы", Symptoms = "Чихание, зуд, сыпь, отеки" }
        };

        // Пациенты
        var patients = new[]
        {
            new Patient { Id = 1, FirstName = "Андрей", LastName = "Смирнов", Age = 45, PhoneNumber = "+79123456789", DoctorId = 1 },
            new Patient { Id = 2, FirstName = "Елена", LastName = "Попова", Age = 32, PhoneNumber = "+79123456780", DoctorId = 2 },
            new Patient { Id = 3, FirstName = "Дмитрий", LastName = "Козлов", Age = 50, PhoneNumber = "+79123456781", DoctorId = 3 },
            new Patient { Id = 4, FirstName = "Наталья", LastName = "Морозова", Age = 28, PhoneNumber = "+79123456782", DoctorId = 1 },
            new Patient { Id = 5, FirstName = "Виктор", LastName = "Орлов", Age = 60, PhoneNumber = "+79123456783", DoctorId = 3 },
            new Patient { Id = 6, FirstName = "Светлана", LastName = "Волкова", Age = 35, PhoneNumber = "+79123456784", DoctorId = 4 },
            new Patient { Id = 7, FirstName = "Михаил", LastName = "Лебедев", Age = 42, PhoneNumber = "+79123456785", DoctorId = 5 },
            new Patient { Id = 8, FirstName = "Ольга", LastName = "Семенова", Age = 29, PhoneNumber = "+79123456786", DoctorId = 6 },
            new Patient { Id = 9, FirstName = "Александр", LastName = "Павлов", Age = 55, PhoneNumber = "+79123456787", DoctorId = 7 },
            new Patient { Id = 10, FirstName = "Ирина", LastName = "Федорова", Age = 38, PhoneNumber = "+79123456788", DoctorId = 8 }
        };

        // Связи многие-ко-многим между пациентами и заболеваниями
        var patientDiseases = new[]
        {
            // Пациент 1: Андрей Смирнов
            new { PatientsId = 1, DiseasesId = 2 }, // Гипертония
            new { PatientsId = 1, DiseasesId = 3 }, // Сахарный диабет
            
            // Пациент 2: Елена Попова
            new { PatientsId = 2, DiseasesId = 4 }, // Астма
            new { PatientsId = 2, DiseasesId = 7 }, // Мигрень
            
            // Пациент 3: Дмитрий Козлов
            new { PatientsId = 3, DiseasesId = 1 }, // Грипп
            new { PatientsId = 3, DiseasesId = 5 }, // Артрит
            
            // Пациент 4: Наталья Морозова
            new { PatientsId = 4, DiseasesId = 2 }, // Гипертония
            new { PatientsId = 4, DiseasesId = 10 }, // Аллергия
            
            // Пациент 5: Виктор Орлов
            new { PatientsId = 5, DiseasesId = 3 }, // Сахарный диабет
            new { PatientsId = 5, DiseasesId = 5 }, // Артрит
            new { PatientsId = 5, DiseasesId = 8 }, // Остеохондроз
            
            // Пациент 6: Светлана Волкова
            new { PatientsId = 6, DiseasesId = 6 }, // Гастрит
            
            // Пациент 7: Михаил Лебедев
            new { PatientsId = 7, DiseasesId = 9 }, // Пневмония
            new { PatientsId = 7, DiseasesId = 10 }, // Аллергия
            
            // Пациент 8: Ольга Семенова
            new { PatientsId = 8, DiseasesId = 7 }, // Мигрень
            
            // Пациент 9: Александр Павлов
            new { PatientsId = 9, DiseasesId = 2 }, // Гипертония
            new { PatientsId = 9, DiseasesId = 3 }, // Сахарный диабет
            new { PatientsId = 9, DiseasesId = 8 }, // Остеохондроз
            
            // Пациент 10: Ирина Федорова
            new { PatientsId = 10, DiseasesId = 4 } // Астма
        };

        // Добавляем данные в модель
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Disease>().HasData(diseases);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Diseases)
            .WithMany(d => d.Patients)
            .UsingEntity(j => j.HasData(patientDiseases));
    }
}