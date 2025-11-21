using MedicalRecords.API.DTOs.Request;
using MedicalRecords.API.DTOs.Request.Doctor;
using MedicalRecords.Domain.Models;

namespace MedicalRecords.API.DTOs.Mapper;

public static class DoctorMapper
{
    public static Doctor Map(this DoctorCreate model)
    {
        return new Doctor
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Specialization = model.Specialization,
            LicenseNumber = model.LicenseNumber
        };
    }
}