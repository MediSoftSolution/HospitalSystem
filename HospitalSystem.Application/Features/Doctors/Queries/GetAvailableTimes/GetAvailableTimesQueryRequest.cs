using MediatR;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAvailableTimes
{
    public class GetAvailableTimesQueryRequest : IRequest<ICollection<TimeSpan>>
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }

        public GetAvailableTimesQueryRequest(int doctorId, DateTime date)
        {
            DoctorId = doctorId;
            Date = date;
        }
    }
}
