using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler : BaseHandler, IRequestHandler<UpdateDoctorCommandRequest, Unit>
    {
        public UpdateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Doctor>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            var map = mapper.Map<Doctor, UpdateDoctorCommandRequest>(request);

            var doctorSpeciality = await unitOfWork.GetReadRepository<Specialty>()
                .GetAsync(x => x.Id == product.Id);

            await unitOfWork.GetWriteRepository<Specialty>()
                .HardDeleteAsync(doctorSpeciality);

            await unitOfWork.GetWriteRepository<Doctor>().UpdateAsync(map);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
