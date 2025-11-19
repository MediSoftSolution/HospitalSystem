using AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Specialties.Queries.GetSpecialityById
{
    public class GetSpecialityByIdQueryHandler : IRequestHandler<GetSpecialityByIdQueryRequest, GetSpecialityByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetSpecialityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetSpecialityByIdQueryResponse> Handle(GetSpecialityByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var specialities = await unitOfWork
                           .GetReadRepository<Specialty>()
                           .GetAllAsync(include: x => x.Include(d => d.Doctors).Include(d => d.Photo), enableTracking: false);

            var specialityDtos = mapper.Map<GetSpecialityByIdQueryResponse>(specialities);

            return specialityDtos;
        }

    }
}
