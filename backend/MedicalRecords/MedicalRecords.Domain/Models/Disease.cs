using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicalRecords.Domain.Models;

public class Disease
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Symptoms { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Patient> Patients { get; set; } = [];
}