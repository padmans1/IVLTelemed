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
using System.Collections.Generic;
namespace BlazorHero.CleanArchitecture.Application.Features.Reports.Commands.AddEdit
{
    public partial class AddEditReportCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public Visit Visit { get; set; }

        public string Description { get; set; }

        public string RightEyeObservations { get; set; }

        public string LeftEyeObservations { get; set; }

        public List<Image> RightEyeImages { get; set; }

        public List<Image> LeftEyeImages { get; set; }
    }

    public class AddEditReportCommandHandler : IRequestHandler<AddEditReportCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;

        public AddEditReportCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<Result<int>> Handle(AddEditReportCommand command, CancellationToken cancellationToken)
        {
            //var uploadRequest = command.UploadRequest;

            if (command.Id == 0)
            {
                var report = _mapper.Map<BlazorHero.CleanArchitecture.Application.Models.Telemed.Report>(command);
                await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Application.Models.Telemed.Report>().AddAsync(report);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(report.Id, "Report Saved");
            }
            else
            {
                var report = await _unitOfWork.Repository< BlazorHero.CleanArchitecture.Application.Models.Telemed.Report >().GetByIdAsync(command.Id);
                if (report != null)
                {
                    report.Description = command.Description ?? report.Description;
                    report.Visit = command.Visit ?? report.Visit;
                    report.RightEyeObservations = command.RightEyeObservations ?? report.RightEyeObservations;
                    report.LeftEyeObservations = command.LeftEyeObservations
                        ?? report.LeftEyeObservations;
                    report.RightEyeImages = command.RightEyeImages?? report.RightEyeImages;
                    report.LeftEyeImages = command.LeftEyeImages ?? report.LeftEyeImages;

                    await _unitOfWork.Repository<BlazorHero.CleanArchitecture.Application.Models.Telemed.Report>().UpdateAsync(report);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(report.Id, "Report Updated");
                }
                else
                {
                    return Result<int>.Fail("Report Not Found!");
                }

            }
        }
    }
}