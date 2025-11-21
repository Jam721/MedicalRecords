namespace MedicalRecords.API.DTOs.Request.Patient;

public class PatientUpdate
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? PhoneNumber { get; set; } = string.Empty;
}