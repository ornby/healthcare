using HealthcareManagementAPI.Models.DTOs;

namespace HealthcareManagementAPI.Services;

public interface IAreaService
{
    Task<AreaEditDto> GetByIdAsync(int id);
    Task<IEnumerable<AreaListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize);
    Task AddAsync(AreaEditDto dto);
    Task UpdateAsync(AreaEditDto dto);
    Task DeleteAsync(int id);
}