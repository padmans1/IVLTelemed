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

namespace BlazorHero.CleanArchitecture.Application.Features.Images.Commands.AddEdit
{
    public partial class AddEditImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        public decimal Rate { get; set; }
        public int BrandId { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    public class AddEditImageCommandHandler : IRequestHandler<AddEditImageCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;

        public AddEditImageCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<Result<int>> Handle(AddEditImageCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.Barcode}{uploadRequest.Extension}";
            }

            if (command.Id == 0)
            {
                var image = _mapper.Map<Image>(command);
                if (uploadRequest != null)
                {
                    image.ImageInfo.ImageURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Image>().AddAsync(image);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(image.Id, "Image Saved");
            }
            else
            {
                var image = await _unitOfWork.Repository<Image>().GetByIdAsync(command.Id);
                if (image != null)
                {
                    //image.Name = command.Name ?? image.Name;
                    image.Description = command.Description ?? image.Description;
                    if (uploadRequest != null)
                    {
                        image.ImageInfo.ImageURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    await _unitOfWork.Repository<Image>().UpdateAsync(image);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(image.Id, "Image Updated");
                }
                else
                {
                    return Result<int>.Fail("Image Not Found!");
                }

            }
        }
    }
}