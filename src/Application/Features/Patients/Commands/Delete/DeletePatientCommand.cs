using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
using System;

namespace BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.Delete
{
    public class DeletePatientCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Result<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;

            public DeletePatientCommandHandler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
            {
                var patient = await _unitOfWork.Repository<PatientInfo>().GetByIdAsync(command.Id);
                await _unitOfWork.Repository<PatientInfo>().DeleteAsync(patient);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(Convert.ToInt32( patient.Id), "Patient Deleted");
            }
        }
    }
}