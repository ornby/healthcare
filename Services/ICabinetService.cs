using HealthcareManagementAPI.Models.DTOs;

namespace HealthcareManagementAPI.Services;

public interface ICabinetService
{
    Task<CabinetEditDto> GetByIdAsync(int id);
    Task<IEnumerable<CabinetListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize);
    Task AddAsync(CabinetEditDto dto);
    Task UpdateAsync(CabinetEditDto dto);
    Task DeleteAsync(int id);
}