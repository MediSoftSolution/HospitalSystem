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

namespace HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommandHandler : BaseHandler, IRequestHandler<DeleteSpecialityCommandRequest, Unit>
    {
        public DeleteSpecialityCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteSpecialityCommandRequest request, CancellationToken cancellationToken)
        {
            var speciality = await unitOfWork.GetReadRepository<Specialty>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            speciality.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Specialty>().UpdateAsync(speciality);
            await unitOfWork.SaveAsync();

            return Unit.Value;        
        }
    }
}
