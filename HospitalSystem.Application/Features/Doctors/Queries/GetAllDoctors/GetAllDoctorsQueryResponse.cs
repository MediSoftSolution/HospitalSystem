
using HospitalSystem.Application.DTOs;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors
{
    public class GetAllDoctorsQueryResponse
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
