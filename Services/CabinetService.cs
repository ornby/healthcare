using AutoMapper;
using HealthcareManagementAPI.Data;
using HealthcareManagementAPI.Entities;
using HealthcareManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementAPI.Services;

public class CabinetService : ICabinetService
{
    private readonly HealthCareDbContext _context;
    private readonly IMapper _mapper;

    public CabinetService(HealthCareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CabinetEditDto> GetByIdAsync(int id)
    {
        var cabinet = await _context.Cabinets.FindAsync(id);
        return _mapper.Map<CabinetEditDto>(cabinet);
    }

    public async Task<IEnumerable<CabinetListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize)
    {
        var query = _context.Cabinets.AsQueryable();
        query = ascending ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy));
        var cabinets = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return _mapper.Map<IEnumerable<CabinetListDto>>(cabinets);
    }

    public async Task AddAsync(CabinetEditDto dto)
    {
        var cabinet = _mapper.Map<Cabinet>(dto);
        _context.Cabinets.Add(cabinet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CabinetEditDto dto)
    {
        var cabinet = await _context.Cabinets.FindAsync(dto.Id);
        if (cabinet != null)
        {
            _mapper.Map(dto, cabinet);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var cabinet = await _context.Cabinets.FindAsync(id);
        if (cabinet != null)
        {
            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();
        }
    }
}