using MediatR;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetDoctorById
{
    public class GetDoctorByIdQueryRequest : IRequest<IList<GetDoctorByIdQueryResponse>>
    {
        public int DoctorId { get; set; }
    }
}
