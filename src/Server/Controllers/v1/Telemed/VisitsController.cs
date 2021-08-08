using BlazorHero.CleanArchitecture.Application.Features.Visits.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Visits.Commands.Delete;
//using IVLTelemed.CleanArchitecture.Application.Features.Patients.Queries.Export;
using BlazorHero.CleanArchitecture.Application.Features.Visits.Queries.GetAllPaged;
//using IVLTelemed.CleanArchitecture.Application.Features.Patients.Queries.GetProductImage;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Telemed
{
    public class VisitsController : BaseApiController<VisitsController>
    {
        [Authorize(Policy = Permissions.Visits.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString)
        {
            var products = await _mediator.Send(new GetAllVisitsQuery(pageNumber, pageSize, searchString));
            return Ok(products);
        }

        //[Authorize(Policy = Permissions.Products.View)]
        //[HttpGet("image/{id}")]
        //public async Task<IActionResult> GetProductImageAsync(int id)
        //{
        //    var result = await _mediator.Send(new GetProductImageQuery(id));
        //    return Ok(result);
        //}

        [Authorize(Policy = Permissions.Visits.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditVisitCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = Permissions.Visits.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteVisitCommand { Id = id }));
        }

        //[Authorize(Policy = Permissions.Products.View)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportProductsQuery(searchString)));
        //}
    }
}