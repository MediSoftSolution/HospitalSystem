using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Offices.Queries.GetAllOffices
{
    public class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQueryRequest, IList<GetAllOfficesQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllOfficesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllOfficesQueryResponse>> Handle(GetAllOfficesQueryRequest request, CancellationToken cancellationToken)
        {
            var offices = await unitOfWork
                           .GetReadRepository<Office>()
                           .GetAllAsync(include: x => x.Include(d => d.Photos).Include(d => d.WorkingTimes));

            var officeDtos = mapper.Map<IList<GetAllOfficesQueryResponse>>(offices);

            return officeDtos;
        }
    }
}
