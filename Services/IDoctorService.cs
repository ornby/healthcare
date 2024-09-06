using HealthcareManagementAPI.Models.DTOs;

namespace HealthcareManagementAPI.Services;

public interface IDoctorService
{
    Task<IEnumerable<DoctorListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize);
    Task<DoctorEditDto> GetByIdAsync(int id);
    Task AddAsync(DoctorEditDto dto);
    Task UpdateAsync(DoctorEditDto dto);
    Task DeleteAsync(int id);
}