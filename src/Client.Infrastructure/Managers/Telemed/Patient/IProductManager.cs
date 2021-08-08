using BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Requests.Telemed;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Telemed.Patient
{
    public interface IPatientManager : IManager
    {
        Task<PaginatedResult<GetAllPagedPatientsResponse>> GetPatientsAsync(GetAllPagedPatientsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditPatientCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}