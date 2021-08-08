using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
using BlazorHero.CleanArchitecture.Application.Models.Identity;


namespace BlazorHero.CleanArchitecture.Application.Features.Visits.Commands.AddEdit
{
    public partial class AddEditVisitCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public BlazorHeroUser Patient { get; set; }
        public string Description { get; set; }
    }

    public class AddEditVisitCommandHandler : IRequestHandler<AddEditVisitCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;

        public AddEditVisitCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<Result<int>> Handle(AddEditVisitCommand command, CancellationToken cancellationToken)
        {

            if (command.Id == 0)
            {
                var Visit = _mapper.Map<Visit>(command);
                //if (uploadRequest != null)
                //{
                //    Visit.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                //}
                await _unitOfWork.Repository<Visit>().AddAsync(Visit);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(Visit.Id, "Visit Saved");
            }
            else
            {
                var visit = await _unitOfWork.Repository<Visit>().GetByIdAsync(command.Id);
                if (visit != null)
                {
                    visit.Description = command.Description ?? visit.Description;
                    visit.Patient = command.Patient ?? visit.Patient;
                    await _unitOfWork.Repository<Visit>().UpdateAsync(visit);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(visit.Id, "Visit Updated");
                }
                else
                {
                    return Result<int>.Fail("Visit Not Found!");
                }

            }
        }
    }
}