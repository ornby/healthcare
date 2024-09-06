using AutoMapper;
using HealthcareManagementAPI.Data;
using HealthcareManagementAPI.Entities;
using HealthcareManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementAPI.Services;

public class DoctorService : IDoctorService
{
    private readonly HealthCareDbContext _context;
    private readonly IMapper _mapper;

    public DoctorService(HealthCareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DoctorEditDto> GetByIdAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        return _mapper.Map<DoctorEditDto>(doctor);
    }

    public async Task<IEnumerable<DoctorListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize)
    {
        var query = _context.Doctors.AsQueryable();

        query = sortBy switch
        {
            "FullName" => ascending ? query.OrderBy(d => d.FullName) : query.OrderByDescending(d => d.FullName),
            _ => query.OrderBy(d => d.Id)
        };

        return await query.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(d => _mapper.Map<DoctorListDto>(d))
            .ToListAsync();
    }

    public async Task AddAsync(DoctorEditDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DoctorEditDto dto)
    {
        var doctor = _mapper.Map<Doctor>(dto);
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }
}