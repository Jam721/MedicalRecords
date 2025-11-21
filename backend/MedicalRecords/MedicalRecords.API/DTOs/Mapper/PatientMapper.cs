using MedicalRecords.API.DTOs.Request.Patient;
using MedicalRecords.API.DTOs.Response;
using MedicalRecords.Domain.Models;

namespace MedicalRecords.API.DTOs.Mapper;

public static class PatientMapper
{
    public static Patient Map(this PatientCreate model)
    {
        return new Patient
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Age = model.Age,
            PhoneNumber = model.PhoneNumber,
        };
    }

    public static PatientResponse Map(this Patient model)
    {
        return new PatientResponse
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Age = model.Age,
            PhoneNumber = model.PhoneNumber,
            Doctor = model.Doctor,
            Diseases = model.Diseases.Select(d => d.Name).ToList()
        };
    }
}