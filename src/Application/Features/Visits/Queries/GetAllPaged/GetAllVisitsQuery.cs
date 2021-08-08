using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Specifications.Telemed;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Features.Visits.Queries.GetAllPaged
{
    public class GetAllVisitsQuery : IRequest<PaginatedResult<GetAllPagedVisitsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllVisitsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    public class GGetAllVisitsQueryHandler : IRequestHandler<GetAllVisitsQuery, PaginatedResult<GetAllPagedVisitsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GGetAllVisitsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedVisitsResponse>> Handle(GetAllVisitsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Visit, GetAllPagedVisitsResponse>> expression = e => new GetAllPagedVisitsResponse
            {
                Id = e.Id
            };
            var VisitFilterSpec = new VisitFilterSpecification(request.SearchString);
            var data = await _unitOfWork.Repository<Visit>().Entities
               .Specify(VisitFilterSpec)
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
    }
}