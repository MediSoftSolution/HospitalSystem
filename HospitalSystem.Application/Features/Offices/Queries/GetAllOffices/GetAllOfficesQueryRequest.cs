using MediatR;

namespace HospitalSystem.Application.Features.Offices.Queries.GetAllOffices
{
    public class GetAllOfficesQueryRequest : IRequest<IList<GetAllOfficesQueryResponse>>
    {

    }
}
