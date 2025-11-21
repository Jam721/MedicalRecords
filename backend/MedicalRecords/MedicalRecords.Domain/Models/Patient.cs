using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicalRecords.Domain.Models;

public class Patient
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Range(0, 150)]
    public int Age { get; set; }
    
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    public int? DoctorId { get; set; }
    
    [JsonIgnore]
    public Doctor? Doctor { get; set; }
    
    public List<Disease> Diseases { get; set; } = [];
    
}
