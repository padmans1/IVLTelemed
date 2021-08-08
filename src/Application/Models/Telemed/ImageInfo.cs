using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Application.Models.Telemed
{
    public class ImageInfo: AuditableEntity<int>
    { 
        public string Notes { get; set; }

        public int ImageSide { get; set; }

        public string ImageURL { get; set; }

        public MaskSettings MaskSettings { get; set; }

        public string CameraSettings { get; set; }

       
    }
}
