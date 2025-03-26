using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Domain.Entities;
using AutoMapper;

namespace HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty
{
    public class UpdateSpecialityCommandHandler : BaseHandler, IRequestHandler<UpdateSpecialityCommandRequest, Unit>
    {
        public UpdateSpecialityCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateSpecialityCommandRequest request, CancellationToken cancellationToken)
        {
            var speciality = await unitOfWork.GetReadRepository<Specialty>()
                .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (speciality == null)
            {
                throw new KeyNotFoundException("Speciality not found.");
            }

            speciality.Name = request.Name ?? speciality.Name;
            speciality.PhotoId = request.PhotoId > 0 ? request.PhotoId : speciality.PhotoId;

            if (request.DoctorIds != null && request.DoctorIds.Any())
            {
                speciality.Doctors.Clear();
                var doctors = await unitOfWork.GetReadRepository<Doctor>()
                    .GetAllAsync(d => request.DoctorIds.Contains(d.Id));
                speciality.Doctors.AddRange(doctors);
            }

            await unitOfWork.GetWriteRepository<Specialty>().UpdateAsync(speciality);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
