using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;

namespace BlazorHero.CleanArchitecture.Application.Features.Patients.Queries.GetById
{
  public class GetPatientsByIdQuery : IRequest<Result<GetPatientsByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetPatientsByIdQuery, Result<GetPatientsByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPatientsByIdResponse>> Handle(GetPatientsByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Repository<PatientInfo>().GetByIdAsync(query.Id);
            var mappedBrand = _mapper.Map<GetPatientsByIdResponse>(brand);
            return await Result<GetPatientsByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}
