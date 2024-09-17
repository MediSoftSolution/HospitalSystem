
using HospitalSystem.Domain.Entities;

namespace YoutubeApi.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllDoctorsQueryResponse
    {
        public Guid DoctorId { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string? About { get; set; }
        public Dictionary<DayOfWeek, WorkingTime> WorkingTimes { get; set; }
        public decimal ConsultingFee { get; set; }
        public string Specialty { get; set; }
        public string Office { get; set; }
        public string PhotoUrl { get; set; }
    }
}
