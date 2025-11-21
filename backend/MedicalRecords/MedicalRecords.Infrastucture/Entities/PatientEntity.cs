namespace MedicalRecords.Infrastucture.Entities;

public class PatientEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    
    public int DoctorId { get; set; }
    public DoctorEntity Doctor { get; set; } = null!;
    public List<DiseaseEntity> Diseases { get; set; } = new();
}