using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQueryRequest, IList<GetAllDoctorsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMyMapper mapper;

        public GetAllDoctorsQueryHandler(IUnitOfWork unitOfWork, IMyMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetAllDoctorsQueryResponse>> Handle(GetAllDoctorsQueryRequest request, CancellationToken cancellationToken)
        {
            var doctors = await unitOfWork
                           .GetReadRepository<Doctor>()
                           .GetAllAsync(include: x => x.Include(d => d.Specialty).Include(d => d.Photo));

            var doctorDtos = mapper.Map<IList<GetAllDoctorsQueryResponse>>(doctors);

            return doctorDtos;
        }
    }
}
