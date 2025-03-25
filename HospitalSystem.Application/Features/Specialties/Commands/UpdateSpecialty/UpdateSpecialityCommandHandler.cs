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
            var product = await unitOfWork.GetReadRepository<Specialty>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            var map = mapper.Map<Specialty>(request);

            await unitOfWork.GetWriteRepository<Specialty>().UpdateAsync(map);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
