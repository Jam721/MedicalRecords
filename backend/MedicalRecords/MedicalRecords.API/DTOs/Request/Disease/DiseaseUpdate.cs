namespace MedicalRecords.API.DTOs.Request.Disease;

public class DiseaseUpdate
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Symptoms { get; set; } = string.Empty;
}