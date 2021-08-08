using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Images.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Images.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<AddEditImageCommand, Image>().ReverseMap();
            CreateMap<GetAllPagedImagesResponse, Image>().ReverseMap();
        }
    }
}