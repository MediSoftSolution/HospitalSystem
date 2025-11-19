using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Doctors.Rules;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : BaseHandler, IRequestHandler<CreateDoctorCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly DoctorRules _doctorRules;

        public CreateDoctorCommandHandler(IMapper myMapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, DoctorRules doctorRules) : base(myMapper, unitOfWork, httpContextAccessor)
        {
            _mapper = mapper;
            _doctorRules = doctorRules;
        }

        public async Task<Unit> Handle(CreateDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            await _doctorRules.UserShouldNotBeDoctor(request.UserId, cancellationToken);
            _doctorRules.WorkingTimesShouldNotBeEmpty(request.WorkingTimes);
            _doctorRules.ConsultingFeeShouldBeValid(request.ConsultingFee);
            _doctorRules.WorkingHoursShouldNotOverlap(request.WorkingTimes);
            await _doctorRules.SpecialtyShouldExist(request.SpecialtyId);
            await _doctorRules.OfficeShouldExist(request.OfficeId);

            Doctor doctor = _mapper.Map<Doctor>(request);
            doctor.Fullname = doctor.User.Fullname;

            await unitOfWork.GetWriteRepository<Doctor>().AddAsync(doctor);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
