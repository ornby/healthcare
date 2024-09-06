namespace HealthcareManagementAPI.Models.DTOs;

public class DoctorEditDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int CabinetId { get; set; }
    public int SpecializationId { get; set; }
    public int? AreaId { get; set; }
}