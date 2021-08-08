using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
namespace BlazorHero.CleanArchitecture.Application.Specifications.Telemed
{
    public class ReportFilterSpecification : HeroSpecification<Report>
    {
        public ReportFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Description != null && (p.Description.Contains(searchString) || p.LeftEyeObservations.Contains(searchString) || p.RightEyeObservations.Contains(searchString));
            }
            else
            {
                Criteria = p => p.Description != null;
            }
        }
    }
}