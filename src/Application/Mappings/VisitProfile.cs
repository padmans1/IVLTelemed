using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Visits.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Visits.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<AddEditVisitCommand, Visit>().ReverseMap();
            CreateMap<GetAllPagedVisitsResponse, Visit>().ReverseMap();
        }
    }
}