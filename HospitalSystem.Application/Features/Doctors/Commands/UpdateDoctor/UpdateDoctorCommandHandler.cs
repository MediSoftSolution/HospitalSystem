using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;
using AutoMapper;

namespace HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler : BaseHandler, IRequestHandler<UpdateDoctorCommandRequest, Unit>
    {
        public UpdateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            var doctor = await unitOfWork.GetReadRepository<Doctor>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (doctor == null)
                throw new Exception("Doctor not found!");

            mapper.Map(request, doctor);

            if (request.WorkingTimes != null && request.WorkingTimes.Any())
            {
                doctor.WorkingTimes = request.WorkingTimes
                    .Select(wt => new WorkingTime
                    {
                        Day = wt.Key,
                        Start = TimeSpan.Parse(wt.Value.Start),
                        End = TimeSpan.Parse(wt.Value.End)
                    }).ToList();
            }

            await unitOfWork.GetWriteRepository<Doctor>().UpdateAsync(doctor);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }

    }
}
