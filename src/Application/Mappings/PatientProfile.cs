using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<AddEditPatientCommand, PatientInfo>().ReverseMap();
            CreateMap<GetAllPagedPatientsResponse, PatientInfo>().ReverseMap();
        }
    }
}