using HealthcareManagementAPI.Models.DTOs;
using HealthcareManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementAPI.Controllers;

[Route("api/[controller]")]
public class AreasController : ControllerBase
{
    private readonly IAreaService _areaService;

    public AreasController(IAreaService areaService)
    {
        _areaService = areaService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AreaEditDto>> GetByIdAsync(int id)
    {
        var area = await _areaService.GetByIdAsync(id);
        if (area == null)
        {
            return NotFound();
        }
        return Ok(area);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AreaListDto>>> GetAllAsync([FromQuery] string sortBy = "Id", [FromQuery] bool ascending = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var areas = await _areaService.GetAllAsync(sortBy, ascending, page, pageSize);
        return Ok(areas);
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] AreaEditDto dto)
    {
        await _areaService.AddAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.Id }, dto);

    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] AreaEditDto dto)
    {
        await _areaService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _areaService.DeleteAsync(id);
        return NoContent();
    }
}
