using HealthcareManagementAPI.Models.DTOs;
using HealthcareManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementAPI.Controllers;

[Route("api/[controller]")]
public class CabinetsController : ControllerBase
{
    private readonly ICabinetService _cabinetService;

    public CabinetsController(ICabinetService cabinetService)
    {
        _cabinetService = cabinetService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CabinetEditDto>> GetByIdAsync(int id)
    {
        var cabinet = await _cabinetService.GetByIdAsync(id);
        if (cabinet == null)
        {
            return NotFound();
        }
        return Ok(cabinet);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CabinetListDto>>> GetAllAsync([FromQuery] string sortBy = "Id", [FromQuery] bool ascending = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var cabinets = await _cabinetService.GetAllAsync(sortBy, ascending, page, pageSize);
        return Ok(cabinets);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] CabinetEditDto dto)
    {
        await _cabinetService.AddAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.Id }, dto);
    }


    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] CabinetEditDto dto)
    {
        await _cabinetService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _cabinetService.DeleteAsync(id);
        return NoContent();
    }
}
