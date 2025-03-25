using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Domain.Entities;
using AutoMapper;

namespace HospitalSystem.Application.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandHandler : BaseHandler, IRequestHandler<DeleteDoctorCommandRequest, Unit>
    {
        public DeleteDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            var doctor = await unitOfWork.GetReadRepository<Doctor>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            doctor.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Doctor>().UpdateAsync(doctor);
            await unitOfWork.SaveAsync();

            return Unit.Value;        
        }
    }
}
