using MediatR;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors
{
    public class GetAllDoctorsQueryRequest : IRequest<IList<GetAllDoctorsQueryResponse>>
    {

    }
}
