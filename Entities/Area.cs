namespace HealthcareManagementAPI.Entities;

public class Area
{
    public int Id { get; set; }
    public string Number { get; set; }
    public ICollection<Patient> Patients { get; set; }
    public ICollection<Doctor> Doctors { get; set; }
}