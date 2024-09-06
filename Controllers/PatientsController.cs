using HealthcareManagementAPI.Models.DTOs;
using HealthcareManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientEditDto>> GetByIdAsync(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientListDto>>> GetAllAsync([FromQuery] string sortBy = "Id",
        [FromQuery] bool ascending = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var patients = await _patientService.GetAllAsync(sortBy, ascending, page, pageSize);
        return Ok(patients);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] PatientEditDto dto)
    {
        await _patientService.AddAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.Id }, dto);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] PatientEditDto dto)
    {
        await _patientService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _patientService.DeleteAsync(id);
        return NoContent();
    }
}