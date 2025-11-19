using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Doctors.Rules;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler : BaseHandler, IRequestHandler<UpdateDoctorCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly DoctorRules _doctorRules;

        public UpdateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, DoctorRules doctorRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _mapper = mapper;
            _doctorRules = doctorRules;
        }

        public async Task<Unit> Handle(UpdateDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            var doctor = await unitOfWork.GetReadRepository<Doctor>()
                .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (doctor == null)
                throw new Exception("Doctor not found!");

            await _doctorRules.SpecialtyShouldExist(request.SpecialtyId);
            await _doctorRules.OfficeShouldExist(request.OfficeId);
            _doctorRules.ConsultingFeeShouldBeValid(request.ConsultingFee);

            _mapper.Map(request, doctor);

            if (request.WorkingTimes != null && request.WorkingTimes.Any())
            {
                doctor.WorkingTimes = request.WorkingTimes
                    .ToDictionary(
                        wt => wt.Key,
                        wt => new WorkingTime
                        {
                            Start = TimeSpan.Parse(wt.Value.Start),
                            End = TimeSpan.Parse(wt.Value.End)
                        }
                    );

                _doctorRules.WorkingTimesShouldNotBeEmpty(doctor.WorkingTimes);
                _doctorRules.WorkingHoursShouldNotOverlap(doctor.WorkingTimes);
            }

            await unitOfWork.GetWriteRepository<Doctor>().UpdateAsync(doctor);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }

}
