using MedicalRecords.Application.Interfaces;
using MedicalRecords.Application.Interfaces.Services;
using MedicalRecords.Domain.Models;

namespace MedicalRecords.Application.Services;

public class DiseaseService(IRepository<Disease> diseaseRepository) : IDiseaseService
{
    public async Task<List<Disease>> GetAllDiseasesAsync(CancellationToken cancellationToken)
    {
        return await diseaseRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Disease?> GetDiseaseByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await diseaseRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Disease> CreateDiseaseAsync(Disease disease, CancellationToken cancellationToken)
    {
        await diseaseRepository.AddAsync(disease, cancellationToken);
        return disease;
    }

    public async Task<Disease?> UpdateDiseaseAsync(int id, Action<Disease> updateAction, CancellationToken cancellationToken)
    {
        var disease = await diseaseRepository.GetByIdAsync(id, cancellationToken);
        if (disease is null) return null;

        updateAction(disease);
        await diseaseRepository.UpdateAsync(disease, cancellationToken);
        return disease;
    }

    public async Task<Disease?> DeleteDiseaseAsync(int id, CancellationToken cancellationToken)
    {
        var disease = await diseaseRepository.GetByIdAsync(id, cancellationToken);
        if (disease is null) return null;

        await diseaseRepository.DeleteAsync(id, cancellationToken);
        return disease;
    }
}