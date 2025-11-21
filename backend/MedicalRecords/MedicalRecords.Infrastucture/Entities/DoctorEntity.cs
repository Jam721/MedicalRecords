namespace MedicalRecords.Infrastucture.Entities;

public class DoctorEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    
    public List<PatientEntity> Patients { get; set; } = new();
}