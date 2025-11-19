using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Doctors.Exceptions;
using HospitalSystem.Application.Features.Doctors.Constants;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Doctors.Rules
{
    public class DoctorRules : BaseRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UserShouldNotBeDoctor(Guid userId, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.GetReadRepository<Doctor>().GetAsync(x => x.UserId == userId);
            if (doctor != null)
                throw new DoctorBusinessException(DoctorMessages.UserAlreadyDoctor);
        }

        public void WorkingTimesShouldNotBeEmpty(Dictionary<DayOfWeek, WorkingTime> workingTimes)
        {
            if (workingTimes == null || !workingTimes.Any())
                throw new DoctorBusinessException(DoctorMessages.WorkingTimesEmpty);
        }

        public void ConsultingFeeShouldBeValid(decimal fee)
        {
            if (fee <= 0)
                throw new DoctorBusinessException(DoctorMessages.InvalidConsultingFee);
        }

        public async Task SpecialtyShouldExist(int? specialtyId)
        {
            if (specialtyId.HasValue)
            {
                var exists = await _unitOfWork.GetReadRepository<Specialty>()
                                               .GetAsync(x => x.Id == specialtyId.Value);

                if (exists is null)
                    throw new DoctorBusinessException(DoctorMessages.SpecialtyNotFound);
            }
        }

        public async Task OfficeShouldExist(int? officeId)
        {
            if (officeId.HasValue)
            {
                var exists = await _unitOfWork.GetReadRepository<Office>()
                                               .GetAsync(x => x.Id == officeId.Value);

                if (exists is null)
                    throw new DoctorBusinessException(DoctorMessages.OfficeNotFound);
            }
        }

        public void WorkingHoursShouldNotOverlap(Dictionary<DayOfWeek, WorkingTime> times)
        {
            foreach (var entry in times)
            {
                var time = entry.Value;
                if (time.Start >= time.End)
                    throw new DoctorBusinessException(DoctorMessages.InvalidWorkingHours);
            }
        }
    }
}
