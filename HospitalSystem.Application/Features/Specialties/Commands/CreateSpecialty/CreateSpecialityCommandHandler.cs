using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Specialties.Commands.CreateSpecialty;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality
{
    public class CreateSpecialityCommandHandler : BaseHandler, IRequestHandler<CreateSpecialityCommandRequest, CreateSpecialityCommandResponse>
    {
        public CreateSpecialityCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateSpecialityCommandResponse> Handle(CreateSpecialityCommandRequest request, CancellationToken cancellationToken)
        {
            Specialty specialty = await unitOfWork.GetReadRepository<Specialty>().GetAsync(s => s.Name == request.Name);

            if (specialty is null)
            {
                return new CreateSpecialityCommandResponse(int.MinValue, false, 
                    "Speciality already existed with the same name!");
            }

            specialty = new Specialty
            {
                Name = request.Name,
                PhotoId = request.PhotoId
            };

            await unitOfWork.GetWriteRepository<Specialty>().AddAsync(specialty);

            return new CreateSpecialityCommandResponse(specialty.Id, true, "Speciality created successfully.");
        }
    }
}
