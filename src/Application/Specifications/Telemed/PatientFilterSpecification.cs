using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
namespace BlazorHero.CleanArchitecture.Application.Specifications.Telemed
{
    public class PatientFilterSpecification : HeroSpecification<PatientInfo>
    {
        public PatientFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Patient.FirstName != null && (p.Patient.FirstName.Contains(searchString) || p.Patient.LastName.Contains(searchString) || p.MRN.Contains(searchString) || p.Patient.Gender.Contains(searchString));
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}