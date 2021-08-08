using BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Requests.Telemed;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Telemed.Patient
{
    public class PatientManager : IPatientManager
    {
        private readonly HttpClient _httpClient;

        public PatientManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.ProductsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.ProductsEndpoints.Export
                : Routes.ProductsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> GetProductImageAsync(int id)
        {
            var response = await _httpClient.GetAsync(Routes.ProductsEndpoints.GetProductImage(id));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedPatientsResponse>> GetPatientsAsync(GetAllPagedPatientsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.PatientsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedPatientsResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditPatientCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ProductsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}