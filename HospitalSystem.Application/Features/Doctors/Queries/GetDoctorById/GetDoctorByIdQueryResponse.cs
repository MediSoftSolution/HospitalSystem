using HospitalSystem.Application.DTOs;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetDoctorById
{
    public class GetDoctorByIdQueryResponse
    {
        public int DoctorId { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string? About { get; set; }
        public Dictionary<DayOfWeek, WorkingTimeDto> WorkingTimes { get; set; }
        public decimal ConsultingFee { get; set; }
        public int SpecialtyId { get; set; }
        public int OfficeId { get; set; }
        public int PhotoId { get; set; }
    }
}
