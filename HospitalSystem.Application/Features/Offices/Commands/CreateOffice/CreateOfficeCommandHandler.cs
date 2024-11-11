using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Offices.Commands.CreateOffice
{
    public class CreateOfficeCommandHandler : BaseHandler, IRequestHandler<CreateOfficeCommandRequest, Unit>
    {
        public CreateOfficeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateOfficeCommandRequest request, CancellationToken cancellationToken)
        {

            Office office = mapper.Map<Office, CreateOfficeCommandRequest>(request);

            await unitOfWork.GetWriteRepository<Office>().AddAsync(office);

            return Unit.Value;
        }
    }
}
