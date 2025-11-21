using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicalRecords.Domain.Models;

public class Doctor
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Specialization { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LicenseNumber { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Patient> Patients { get; set; } = [];
}