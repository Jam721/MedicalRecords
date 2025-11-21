namespace MedicalRecords.Infrastucture.Entities;

public class DiseaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Symptoms { get; set; } = string.Empty;
    
    public int PatientId { get; set; }
    public PatientEntity Patient { get; set; } = null!;
}