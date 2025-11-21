using MedicalRecords.Domain.Models;

namespace MedicalRecords.Application.Interfaces.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken);
    Task<Doctor?> GetDoctorByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Doctor>> GetDoctorsBySpecializationAsync(string specialization, CancellationToken cancellationToken);
    Task<Doctor> CreateDoctorAsync(Doctor doctor, CancellationToken cancellationToken);
    Task<Doctor?> UpdateDoctorAsync(int id, Action<Doctor> updateAction, CancellationToken cancellationToken);
    Task<Doctor?> DeleteDoctorAsync(int id, CancellationToken cancellationToken);
    Task<Doctor?> AddPatientToDoctorAsync(int doctorId, int patientId, CancellationToken cancellationToken);
    Task<Doctor?> RemovePatientFromDoctorAsync(int doctorId, int patientId, CancellationToken cancellationToken);
}