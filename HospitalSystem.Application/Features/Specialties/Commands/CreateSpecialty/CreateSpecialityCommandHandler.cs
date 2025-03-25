using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality
{
    public class CreateSpecialityCommandHandler : BaseHandler, IRequestHandler<CreateSpecialityCommandRequest, Unit>
    {
        public CreateSpecialityCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateSpecialityCommandRequest request, CancellationToken cancellationToken)
        {
          

            Specialty specialty = mapper.Map<Specialty>(request);

            await unitOfWork.GetWriteRepository<Specialty>().AddAsync(specialty);

            return Unit.Value;
        }
    }
}
