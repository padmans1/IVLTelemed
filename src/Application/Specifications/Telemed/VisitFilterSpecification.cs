using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
namespace BlazorHero.CleanArchitecture.Application.Specifications.Telemed
{
    public class VisitFilterSpecification : HeroSpecification<Visit>
    {
        public VisitFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Description != null && (p.Description.Contains(searchString));
            }
            else
            {
                Criteria = p => p.Description != null;
            }
        }
    }
}