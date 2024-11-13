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

namespace HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty
{
    public class UpdateSpecialityCommandHandler : BaseHandler, IRequestHandler<UpdateSpecialityCommandRequest, Unit>
    {
        public UpdateSpecialityCommandHandler(IMyMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateSpecialityCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Specialty>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            var map = mapper.Map<Specialty, UpdateSpecialityCommandRequest>(request);

            await unitOfWork.GetWriteRepository<Specialty>().UpdateAsync(map);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
