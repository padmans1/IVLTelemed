using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Reports.Commands.Delete
{
    public class DeleteReportCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, Result<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;

            public DeleteReportCommandHandler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteReportCommand command, CancellationToken cancellationToken)
            {
                var report = await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Application.Models.Telemed.Report>().GetByIdAsync(command.Id);
                await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Application.Models.Telemed.Report>().DeleteAsync(report);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(report.Id, "Report Deleted");
            }
        }
    }
}