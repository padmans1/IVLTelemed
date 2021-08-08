using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Specifications;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Features.Report.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Features.Reports.Queries.GetAllPaged
{
    public class GetAllReportsQuery : IRequest<PaginatedResult<GetAllPagedReportsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllReportsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    public class GGetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, PaginatedResult<GetAllPagedReportsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GGetAllReportsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedReportsResponse>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func< BlazorHero.CleanArchitecture.Application.Models.Telemed.Report, GetAllPagedReportsResponse>> expression = e => new GetAllPagedReportsResponse
            {
                Id = e.Id
            };
            //var ReportFilterSpec = new ReportFilterSpecification(request.SearchString);
            var data = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Application.Models.Telemed.Report>().Entities
               //.Specify(ReportFilterSpec)
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
    }
}