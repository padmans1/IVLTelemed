using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Report.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Reports.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<AddEditReportCommand, Report>().ReverseMap();
            CreateMap<GetAllPagedReportsResponse, Report>().ReverseMap();
        }
    }
}