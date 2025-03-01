using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Offices.Commands.CreateOffice
{
    public class CreateOfficeCommandHandler : BaseHandler, IRequestHandler<CreateOfficeCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        public CreateOfficeCommandHandler(IMyMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper map) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _mapper = map;
        }

        public async Task<Unit> Handle(CreateOfficeCommandRequest request, CancellationToken cancellationToken)
        {

            Office office = mapper.Map<Office>(request);

            await unitOfWork.GetWriteRepository<Office>().AddAsync(office);
            
            unitOfWork.Save();

            return Unit.Value;
        }
    }
}
