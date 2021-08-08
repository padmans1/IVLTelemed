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

namespace BlazorHero.CleanArchitecture.Application.Features.Patients.Commands.AddEdit
{
    public partial class AddEditPatientCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        
        public string MRN { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public String Gender { get; set; }
        public string Description { get; set; }


        public UploadRequest UploadRequest { get; set; }
    }

    public class AddEditPatientCommandHandler : IRequestHandler<AddEditPatientCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;

        public AddEditPatientCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<Result<int>> Handle(AddEditPatientCommand command, CancellationToken cancellationToken)
        {
            var uploadRequest = command.UploadRequest;

            if (command.Id == 0)
            {
                var patient = _mapper.Map<PatientInfo>(command);
                await _unitOfWork.Repository<PatientInfo>().AddAsync(patient);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(Convert.ToInt32( patient.Patient.Id), "Patient Saved");
            }
            else
            {
                var patientInfo = await _unitOfWork.Repository<PatientInfo>().GetByIdAsync(command.Id);
                if (patientInfo != null)
                {
                    patientInfo.MRN = command.MRN ?? patientInfo.MRN;
                    patientInfo.Patient.FirstName = command.FirstName ?? patientInfo.Patient.FirstName;
                    patientInfo.Patient.LastName = command.LastName ?? patientInfo.Patient.LastName;
                    if(command.DOB != null)
                        patientInfo.Patient.DOB =  command.DOB;

                    patientInfo.Patient.Gender = command.Gender ?? command.Gender;
                    patientInfo.Description = command.Description ?? patientInfo.Description;
                    
                    await _unitOfWork.Repository<PatientInfo>().UpdateAsync(patientInfo);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(Convert.ToInt32(patientInfo.Id), "Patient Updated");
                }
                else
                {
                    return Result<int>.Fail("Patient Not Found!");
                }

            }
        }
    }
}