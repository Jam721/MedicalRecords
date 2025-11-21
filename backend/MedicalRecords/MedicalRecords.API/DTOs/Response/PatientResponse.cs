using MedicalRecords.Domain.Models;

namespace MedicalRecords.API.DTOs.Response;

public class PatientResponse
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    

    public string LastName { get; set; } = string.Empty;
    
    public int Age { get; set; }
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public Doctor? Doctor { get; set; }
    
    public List<string> Diseases { get; set; } = [];
}