using System;
namespace BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged
{
    public class GetAllPagedPatientsResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public string MRN { get; set; }

        public DateTime DOB {get; set;}

        public string Gender { get; set; }
    }
}