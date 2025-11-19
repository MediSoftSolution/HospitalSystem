using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Domain.Entities;
using AutoMapper;

namespace HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommandHandler : BaseHandler, IRequestHandler<DeleteSpecialityCommandRequest, bool>
    {
        public DeleteSpecialityCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<bool> Handle(DeleteSpecialityCommandRequest request, CancellationToken cancellationToken)
        {
            var speciality = await unitOfWork.GetReadRepository<Specialty>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            speciality.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Specialty>().UpdateAsync(speciality);
            await unitOfWork.SaveAsync();

            return true;        
        }
    }
}
