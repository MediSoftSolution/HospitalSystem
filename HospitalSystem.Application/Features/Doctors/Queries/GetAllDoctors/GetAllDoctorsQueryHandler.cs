using AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQueryRequest, IList<GetAllDoctorsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllDoctorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllDoctorsQueryResponse>> Handle(GetAllDoctorsQueryRequest request, CancellationToken cancellationToken)
        {
            var doctors = await unitOfWork
                           .GetReadRepository<Doctor>()
                           .GetAllAsync(include: x => x.Include(d => d.User).Include(d => d.WorkingTimes), 
                           enableTracking: false);

            var doctorDtos = mapper.Map<List<GetAllDoctorsQueryResponse>>(doctors);

            return doctorDtos;
        }
    }
}
