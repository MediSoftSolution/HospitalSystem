using AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Specialties.Queries.GetAllSpecialities
{
    public class GetAllSpecialitiesQueryHandler : IRequestHandler<GetAllSpecialitiesQueryRequest, IList<GetAllSpecialitiesQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllSpecialitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllSpecialitiesQueryResponse>> Handle(GetAllSpecialitiesQueryRequest request, CancellationToken cancellationToken)
        {
            var specialities = await unitOfWork
                           .GetReadRepository<Specialty>()
                           .GetAllAsync(include: x => x.Include(d => d.Doctors).Include(d => d.Photo));

            var specialityDtos = mapper.Map<List<GetAllSpecialitiesQueryResponse>>(specialities);

            return specialityDtos;
        }
    }
}
