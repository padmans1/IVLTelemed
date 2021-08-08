using BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.Delete;
//using IVLTelemed.CleanArchitecture.Application.Features.Patients.Queries.Export;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetById;
//using IVLTelemed.CleanArchitecture.Application.Features.Patients.Queries.GetProductImage;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Telemed
{
    public class PatientsController : BaseApiController<PatientsController>
    {
        [Authorize(Policy = Permissions.Patients.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString)
        {
            var patients = await _mediator.Send(new GetAllPatientsQuery(pageNumber, pageSize, searchString));
            return Ok(patients);
        }

        [Authorize(Policy = Permissions.Patients.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPatientsByIdQuery() {Id = id });
            return Ok(result);
        }

        [Authorize(Policy = Permissions.Patients.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditPatientCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.Patients.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePatientCommand { Id = id }));
        }

        //[Authorize(Policy = Permissions.Products.View)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportProductsQuery(searchString)));
        //}
    }
}