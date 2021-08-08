using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Application.Models.Telemed
{
    public class Image : AuditableEntity<int>
    {
        public BlazorHeroUser Patient { get; set; }
        public Visit Visit { get; set; }

        public string Description { get; set; }

        public ImageInfo ImageInfo { get; set; }

    }
}
