using AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetDoctorById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQueryRequest, IList<GetDoctorByIdQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IList<GetDoctorByIdQueryResponse>> Handle(GetDoctorByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var doctor = await unitOfWork
                           .GetReadRepository<Doctor>()
                           .GetAsync(predicate: d => d.Id == request.DoctorId,   
                           include: x => x.Include(d => d.User).Include(d => d.WorkingTimes),
                           enableTracking: false);

            var doctorDtos = mapper.Map<List<GetDoctorByIdQueryResponse>>(doctor);

            return doctorDtos;
        }
    }
}
