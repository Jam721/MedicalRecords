using MedicalRecords.API.DTOs.Mapper;
using MedicalRecords.API.DTOs.Request.Disease;
using MedicalRecords.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.API.Controllers;

[ApiController]
[Route("api/diseases")]
public class DiseasesController(IDiseaseService diseaseService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAllDiseases(CancellationToken cancellationToken)
    {
        var diseases = await diseaseService.GetAllDiseasesAsync(cancellationToken);
        return Ok(diseases);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDisease(int id, CancellationToken cancellationToken)
    {
        var disease = await diseaseService.GetDiseaseByIdAsync(id, cancellationToken);
        return disease is not null ? Ok(disease) : NotFound();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateDisease([FromBody] DiseaseCreate request, CancellationToken cancellationToken)
    {
        var disease = request.Map();
        var result = await diseaseService.CreateDiseaseAsync(disease, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDisease(int id, [FromBody] DiseaseUpdate request, CancellationToken cancellationToken)
    {
        var disease = await diseaseService.UpdateDiseaseAsync(id, d =>
        {
            if (request.Description != null)
                d.Description = request.Description;
            if (request.Name != null)
                d.Name = request.Name;
            if (request.Symptoms != null)
                d.Symptoms = request.Symptoms;
        }, cancellationToken);

        return disease is not null ? Ok(disease) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDisease(int id, CancellationToken cancellationToken)
    {
        var disease = await diseaseService.DeleteDiseaseAsync(id, cancellationToken);
        return disease is not null ? Ok(disease) : NotFound();
    }
}