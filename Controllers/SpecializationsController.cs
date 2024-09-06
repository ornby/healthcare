using HealthcareManagementAPI.Models.DTOs;
using HealthcareManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementAPI.Controllers;

[Route("api/[controller]")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationsController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SpecializationEditDto>> GetByIdAsync(int id)
    {
        var specialization = await _specializationService.GetByIdAsync(id);
        if (specialization == null)
        {
            return NotFound();
        }
        return Ok(specialization);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecializationListDto>>> GetAllAsync([FromQuery] string sortBy = "Id", [FromQuery] bool ascending = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var specializations = await _specializationService.GetAllAsync(sortBy, ascending, page, pageSize);
        return Ok(specializations);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] SpecializationEditDto dto)
    {
        await _specializationService.AddAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.Id }, dto);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] SpecializationEditDto dto)
    {
        await _specializationService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _specializationService.DeleteAsync(id);
        return NoContent();
    }
}
