using BlazorHero.CleanArchitecture.Application.Models.Telemed;
namespace BlazorHero.CleanArchitecture.Application.Features.Images.Queries.GetAllPaged
{
    public class GetAllPagedImagesResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ImageInfo ImageInfo { get; set; }
    }
}