using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Features.Visits.Commands.Delete
{
    public class DeleteVisitCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteVisitCommandHandler : IRequestHandler<DeleteVisitCommand, Result<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;

            public DeleteVisitCommandHandler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteVisitCommand command, CancellationToken cancellationToken)
            {
                var Visit = await _unitOfWork.Repository<Visit>().GetByIdAsync(command.Id);
                await _unitOfWork.Repository<Visit>().DeleteAsync(Visit);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(Visit.Id, "Visit Deleted");
            }
        }
    }
}