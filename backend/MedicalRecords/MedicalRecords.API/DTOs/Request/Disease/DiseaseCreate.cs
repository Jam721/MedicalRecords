using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.API.DTOs.Request.Disease;

public class DiseaseCreate
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Symptoms { get; set; } = string.Empty;
}