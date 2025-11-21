using MedicalRecords.API.DTOs.Mapper;
using MedicalRecords.API.DTOs.Request.Patient;
using MedicalRecords.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.API.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllPatients(CancellationToken cancellationToken)
    {
        var patients = await patientService.GetPatientsAsync(cancellationToken);
        return Ok(patients.Select(p=>p.Map()).ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatient(int id, CancellationToken cancellationToken)
    {
        var patient = await patientService.GetPatientAsync(id, cancellationToken);
        return patient is not null ? Ok(patient.Map()) : NotFound();
    }

    [HttpPost("")]
    public async Task<IActionResult> AddPatient([FromBody] PatientCreate request, CancellationToken cancellationToken)
    {
        var patient = request.Map();
        await patientService.CreatePatientAsync(patient, cancellationToken);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientUpdate request, CancellationToken cancellationToken)
    {
        var patient = await patientService.UpdatePatientAsync(id, p =>
        {
            if (request.FirstName != null) p.FirstName = request.FirstName;
            if (request.LastName != null) p.LastName = request.LastName;
            if (request.Age != null) p.Age = (int)request.Age;
            if (request.PhoneNumber != null) p.PhoneNumber = request.PhoneNumber;
        }, cancellationToken);

        return patient is not null ? Ok() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePatient(int id, CancellationToken cancellationToken)
    {
        var patient = await patientService.DeletePatientAsync(id, cancellationToken);
        return patient is not null ? Ok() : NotFound();
    }

    [HttpPut("{patientId:int}/doctor/{doctorId:int}")]
    public async Task<IActionResult> AddOrChangeDoctor(int patientId, int doctorId, CancellationToken cancellationToken)
    {
        var patient = await patientService.AssignDoctorAsync(patientId, doctorId, cancellationToken);
        return patient is not null ? Ok() : NotFound();
    }

    [HttpPut("{patientId:int}/diseases/{diseaseId:int}")]
    public async Task<IActionResult> AddDisease(int patientId, int diseaseId, CancellationToken cancellationToken)
    {
        var patient = await patientService.AddDiseaseAsync(patientId, diseaseId, cancellationToken);
        return patient is not null ? Ok() : NotFound();
    }

    [HttpDelete("{patientId:int}/diseases/{diseaseId:int}")]
    public async Task<IActionResult> RemoveDisease(int patientId, int diseaseId, CancellationToken cancellationToken)
    {
        var patient = await patientService.RemoveDiseaseAsync(patientId, diseaseId, cancellationToken);
        return patient is not null ? Ok() : NotFound();
    }
}