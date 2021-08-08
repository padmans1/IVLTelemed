namespace BlazorHero.CleanArchitecture.Application.Requests.Telemed
{
    public class GetAllPagedPatientsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}