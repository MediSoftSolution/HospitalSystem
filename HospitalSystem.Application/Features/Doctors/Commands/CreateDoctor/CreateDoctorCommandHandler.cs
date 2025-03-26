using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : BaseHandler, IRequestHandler<CreateDoctorCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        public CreateDoctorCommandHandler(IMapper myMapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(myMapper, unitOfWork, httpContextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(request);
            doctor.Fullname = doctor.User.Fullname;

            await unitOfWork.GetWriteRepository<Doctor>().AddAsync(doctor);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
