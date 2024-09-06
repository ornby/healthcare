using AutoMapper;
using HealthcareManagementAPI.Data;
using HealthcareManagementAPI.Entities;
using HealthcareManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementAPI.Services;

public class AreaService : IAreaService
{
    private readonly HealthCareDbContext _context;
    private readonly IMapper _mapper;

    public AreaService(HealthCareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AreaEditDto> GetByIdAsync(int id)
    {
        var area = await _context.Areas.FindAsync(id);
        return _mapper.Map<AreaEditDto>(area);
    }

    public async Task<IEnumerable<AreaListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize)
    {
        var query = _context.Areas.AsQueryable();
        query = ascending ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy));
        var areas = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return _mapper.Map<IEnumerable<AreaListDto>>(areas);
    }

    public async Task AddAsync(AreaEditDto dto)
    {
        var area = _mapper.Map<Area>(dto);
        _context.Areas.Add(area);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AreaEditDto dto)
    {
        var area = await _context.Areas.FindAsync(dto.Id);
        if (area != null)
        {
            _mapper.Map(dto, area);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var area = await _context.Areas.FindAsync(id);
        if (area != null)
        {
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
        }
    }
}