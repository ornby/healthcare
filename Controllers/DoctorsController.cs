using HealthcareManagementAPI.Models.DTOs;
using HealthcareManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementAPI.Controllers;

[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorEditDto>> GetByIdAsync(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return Ok(doctor);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetAllAsync([FromQuery] string sortBy = "Id", [FromQuery] bool ascending = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var doctors = await _doctorService.GetAllAsync(sortBy, ascending, page, pageSize);
        return Ok(doctors);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] DoctorEditDto dto)
    {
        await _doctorService.AddAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.Id }, dto);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] DoctorEditDto dto)
    {
        await _doctorService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _doctorService.DeleteAsync(id);
        return NoContent();
    }
}