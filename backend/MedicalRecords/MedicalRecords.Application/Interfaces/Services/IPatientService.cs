using MedicalRecords.Domain.Models;

namespace MedicalRecords.Application.Interfaces.Services;

public interface IPatientService
{
    Task<List<Patient>> GetPatientsAsync(CancellationToken cancellationToken);
    Task<Patient?> GetPatientAsync(int id, CancellationToken cancellationToken);
    Task<Patient> CreatePatientAsync(Patient patient, CancellationToken cancellationToken);
    Task<Patient?> UpdatePatientAsync(int id, Action<Patient> updateAction, CancellationToken cancellationToken);
    Task<Patient?> DeletePatientAsync(int id, CancellationToken cancellationToken);
    Task<Patient?> AssignDoctorAsync(int patientId, int doctorId, CancellationToken cancellationToken);
    Task<Patient?> AddDiseaseAsync(int patientId, int diseaseId, CancellationToken cancellationToken);
    Task<Patient?> RemoveDiseaseAsync(int patientId, int diseaseId, CancellationToken cancellationToken);
}