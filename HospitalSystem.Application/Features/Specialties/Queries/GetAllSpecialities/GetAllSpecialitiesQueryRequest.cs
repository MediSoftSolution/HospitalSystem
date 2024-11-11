using MediatR;

namespace HospitalSystem.Application.Features.Specialties.Queries.GetAllSpecialities
{
    public class GetAllSpecialitiesQueryRequest : IRequest<IList<GetAllSpecialitiesQueryResponse>>
    {

    }
}
