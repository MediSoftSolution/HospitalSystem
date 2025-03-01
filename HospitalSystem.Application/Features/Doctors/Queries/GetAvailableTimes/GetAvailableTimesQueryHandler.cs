using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;


namespace HospitalSystem.Application.Features.Doctors.Queries.GetAvailableTimes
{
    public class GetAvailableTimesQueryHandler : IRequestHandler<GetAvailableTimesQueryRequest, ICollection<TimeSpan>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAvailableTimesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<TimeSpan>> Handle(GetAvailableTimesQueryRequest request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.GetReadRepository<Doctor>().GetAsync(d => d.Id == request.DoctorId);
            if (doctor == null)
                return new List<TimeSpan>();

            var bookedTimes = doctor.Appointments
                .Where(a => a.ConsultingDate.Date == request.Date.Date)
                .Select(a => a.ConsultingDate)
                .ToList();

            List<TimeSpan> allPossibleTimes = GenerateWorkingHours();
            var availableTimes = allPossibleTimes.Except(bookedTimes.Select(b => b.TimeOfDay)).ToList(); 
            return availableTimes;
        }

        private List<TimeSpan> GenerateWorkingHours()
        {
            List<TimeSpan> times = new List<TimeSpan>();
            for (int hour = 9; hour < 17; hour++)
            {
                times.Add(new TimeSpan(hour, 0, 0));
                times.Add(new TimeSpan(hour, 30, 0));
            }
            return times;
        }

    }
}
