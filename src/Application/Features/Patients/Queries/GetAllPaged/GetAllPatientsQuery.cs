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

namespace BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetAllPaged
{
    public class GetAllPatientsQuery : IRequest<PaginatedResult<GetAllPagedPatientsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllPatientsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    public class GGetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, PaginatedResult<GetAllPagedPatientsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GGetAllPatientsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedPatientsResponse>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<PatientInfo, GetAllPagedPatientsResponse>> expression = e => new GetAllPagedPatientsResponse
            {
                Id = Convert.ToInt32( e.Id),
                MRN = e.MRN,
                FirstName = e.Patient.FirstName,
                DOB = e.Patient.DOB,
                Gender = e.Patient.Gender,
                LastName = e.Patient.LastName
            };
            var PatientFilterSpec = new PatientFilterSpecification(request.SearchString);
            var data = await _unitOfWork.Repository<PatientInfo>().Entities
               .Specify(PatientFilterSpec)
               .Select(expression)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data;
        }
    }
}