using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Doctors.Rules;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : BaseHandler, IRequestHandler<CreateDoctorCommandRequest, Unit>
    {
        public CreateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateDoctorCommandRequest request, CancellationToken cancellationToken)
        {

            Doctor doctor = mapper.Map<Doctor, CreateDoctorCommandRequest>(request);

            await unitOfWork.GetWriteRepository<Doctor>().AddAsync(doctor);

            return Unit.Value;
        }
    }
}
