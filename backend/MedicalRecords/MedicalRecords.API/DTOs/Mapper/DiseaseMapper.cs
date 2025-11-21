using MedicalRecords.API.DTOs.Request;
using MedicalRecords.API.DTOs.Request.Disease;
using MedicalRecords.Domain.Models;

namespace MedicalRecords.API.DTOs.Mapper;

public static class DiseaseMapper
{
    public static Disease Map(this DiseaseCreate model)
    {
        return new Disease
        {
            Description = model.Description,
            Name = model.Name,
            Symptoms = model.Symptoms,
        };
    }
}