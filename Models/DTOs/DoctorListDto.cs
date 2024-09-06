namespace HealthcareManagementAPI.Models.DTOs;

public class DoctorListDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string CabinetNumber { get; set; }
    public string SpecializationName { get; set; }
    public string AreaNumber { get; set; }
}