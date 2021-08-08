using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Application.Models.Telemed
{
    public class PatientInfo: AuditableEntity<int>
    {
        public BlazorHeroUser Patient { get; set; }
        public string MRN {get; set;}
        
        public string Description { get; set;}

    }
}