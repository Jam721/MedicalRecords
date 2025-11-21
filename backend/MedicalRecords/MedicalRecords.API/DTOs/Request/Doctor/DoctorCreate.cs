using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.API.DTOs.Request.Doctor;

public class DoctorCreate
{
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
}