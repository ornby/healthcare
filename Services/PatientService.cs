using AutoMapper;
using HealthcareManagementAPI.Data;
using HealthcareManagementAPI.Entities;
using HealthcareManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementAPI.Services;

public class PatientService : IPatientService
{
    private readonly HealthCareDbContext _context;
    private readonly IMapper _mapper;

    public PatientService(HealthCareDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PatientEditDto> GetByIdAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        return _mapper.Map<PatientEditDto>(patient);
    }

    public async Task<IEnumerable<PatientListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize)
    {
        var query = _context.Patients.AsQueryable();

        query = sortBy switch
        {
            "LastName" => ascending ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName),
            "FirstName" => ascending ? query.OrderBy(p => p.FirstName) : query.OrderByDescending(p => p.FirstName),
            _ => query.OrderBy(p => p.Id)
        };

        return await query.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(p => _mapper.Map<PatientListDto>(p))
            .ToListAsync();
    }

    public async Task AddAsync(PatientEditDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PatientEditDto dto)
    {
        var patient = _mapper.Map<Patient>(dto);
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
}