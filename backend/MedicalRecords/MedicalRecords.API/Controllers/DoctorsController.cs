using MedicalRecords.API.DTOs.Mapper;
using MedicalRecords.API.DTOs.Request;
using MedicalRecords.API.DTOs.Request.Doctor;
using MedicalRecords.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.API.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController(IDoctorService doctorService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        var doctors = await doctorService.GetAllDoctorsAsync(cancellationToken);
        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDoctorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.GetDoctorByIdAsync(id, cancellationToken);
        return doctor is not null ? Ok(doctor) : NotFound();
    }
    
    [HttpGet("specialization/{specialization}")]
    public async Task<IActionResult> GetBySpecialization(string specialization, CancellationToken cancellationToken)
    {
        var doctors = await doctorService.GetDoctorsBySpecializationAsync(specialization, cancellationToken);
        return Ok(doctors);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreate request, CancellationToken cancellationToken)
    {
        var doctor = request.Map();
        var result = await doctorService.CreateDoctorAsync(doctor, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorUpdate request, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.UpdateDoctorAsync(id, d =>
        {
            if (request.Specialization != null)
                d.Specialization = request.Specialization;
            if (request.FirstName != null)
                d.FirstName = request.FirstName;
            if (request.LastName != null)
                d.LastName = request.LastName;
            if (request.LicenseNumber != null)
                d.LicenseNumber = request.LicenseNumber;
        }, cancellationToken);

        return doctor is not null ? Ok(doctor) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDoctor(int id, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.DeleteDoctorAsync(id, cancellationToken);
        return doctor is not null ? Ok(doctor) : NotFound();
    }

    [HttpPut("{doctorId:int}/patients/{patientId:int}")]
    public async Task<IActionResult> AddPatientToDoctor(int doctorId, int patientId, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.AddPatientToDoctorAsync(doctorId, patientId, cancellationToken);
        return doctor is not null ? Ok(doctor) : NotFound();
    }
    
    [HttpDelete("{doctorId:int}/patients/{patientId:int}")]
    public async Task<IActionResult> RemovePatientFromDoctor(int doctorId, int patientId, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.RemovePatientFromDoctorAsync(doctorId, patientId, cancellationToken);
        return doctor is not null ? Ok(doctor) : NotFound();
    }
}