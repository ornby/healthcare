using HealthcareManagementAPI.Models.DTOs;

namespace HealthcareManagementAPI.Services;

public interface IPatientService
{
    Task<PatientEditDto> GetByIdAsync(int id);
    Task<IEnumerable<PatientListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize);
    Task AddAsync(PatientEditDto dto);
    Task UpdateAsync(PatientEditDto dto);
    Task DeleteAsync(int id);
}