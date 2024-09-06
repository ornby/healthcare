using HealthcareManagementAPI.Models.DTOs;

namespace HealthcareManagementAPI.Services;

public interface ISpecializationService
{
    Task<SpecializationEditDto> GetByIdAsync(int id);
    Task<IEnumerable<SpecializationListDto>> GetAllAsync(string sortBy, bool ascending, int page, int pageSize);
    Task AddAsync(SpecializationEditDto dto);
    Task UpdateAsync(SpecializationEditDto dto);
    Task DeleteAsync(int id);
}