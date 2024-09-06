using AutoMapper;
using HealthcareManagementAPI.Entities;
using HealthcareManagementAPI.Models.DTOs;

namespace HealthcareManagementAPI.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientEditDto>();
        CreateMap<PatientEditDto, Patient>();
        CreateMap<Doctor, DoctorEditDto>();
        CreateMap<DoctorEditDto, Doctor>();

        CreateMap<Patient, PatientListDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.MiddleName}"))
            .ForMember(dest => dest.AreaNumber, opt => opt.MapFrom(src => src.Area.Number));

        CreateMap<Doctor, DoctorListDto>()
            .ForMember(dest => dest.CabinetNumber, opt => opt.MapFrom(src => src.Cabinet.Number))
            .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name))
            .ForMember(dest => dest.AreaNumber, opt => opt.MapFrom(src => src.Area.Number));
        
        CreateMap<Area, AreaEditDto>().ReverseMap();
        CreateMap<Area, AreaListDto>();

        CreateMap<Specialization, SpecializationEditDto>().ReverseMap();
        CreateMap<Specialization, SpecializationListDto>();

        CreateMap<Cabinet, CabinetEditDto>().ReverseMap();
        CreateMap<Cabinet, CabinetListDto>();
    }
}