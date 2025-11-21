using MedicalRecords.Domain.Models;

namespace MedicalRecords.Application.Interfaces.Services;

public interface IDiseaseService
{
    Task<List<Disease>> GetAllDiseasesAsync(CancellationToken cancellationToken);
    Task<Disease?> GetDiseaseByIdAsync(int id, CancellationToken cancellationToken);
    Task<Disease> CreateDiseaseAsync(Disease disease, CancellationToken cancellationToken);
    Task<Disease?> UpdateDiseaseAsync(int id, Action<Disease> updateAction, CancellationToken cancellationToken);
    Task<Disease?> DeleteDiseaseAsync(int id, CancellationToken cancellationToken);
}