using AutoMapper;
using HealthcareManagementAPI.Data;
using HealthcareManagementAPI.Entities;
using HealthcareManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementAPI.Services;

public class SpecializationService : ISpecializationService
{
    private readonly HealthCareDbContext _context;
    private readonly IMapper _mapper;

    public SpecializationService(HealthCareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SpecializationEditDto> GetByIdAsync(int id)
    {
        var specialization = await _context.Specializations.FindAsync(id);
        return _mapper.Map<SpecializationEditDto>(specialization);
    }

    public async Task<IEnumerable<SpecializationListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize)
    {
        var query = _context.Specializations.AsQueryable();
        query = ascending ? query.OrderBy(e => EF.Property<object>(e, sortBy)) : query.OrderByDescending(e => EF.Property<object>(e, sortBy));
        var specializations = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return _mapper.Map<IEnumerable<SpecializationListDto>>(specializations);
    }

    public async Task AddAsync(SpecializationEditDto dto)
    {
        var specialization = _mapper.Map<Specialization>(dto);
        _context.Specializations.Add(specialization);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SpecializationEditDto dto)
    {
        var specialization = await _context.Specializations.FindAsync(dto.Id);
        if (specialization != null)
        {
            _mapper.Map(dto, specialization);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var specialization = await _context.Specializations.FindAsync(id);
        if (specialization != null)
        {
            _context.Specializations.Remove(specialization);
            await _context.SaveChangesAsync();
        }
    }
}