using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;

namespace BlazorHero.CleanArchitecture.Application.Features.Images.Commands.Delete
{
    public class DeleteImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;

            public DeleteImageCommandHandler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteImageCommand command, CancellationToken cancellationToken)
            {
                var image = await _unitOfWork.Repository<Image>().GetByIdAsync(command.Id);
                await _unitOfWork.Repository<Image>().DeleteAsync(image);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(image.Id, "Image Deleted");
            }
        }
    }
}