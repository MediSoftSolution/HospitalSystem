using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : BaseHandler, IRequestHandler<CreateDoctorCommandRequest, Unit>
    {
        public CreateDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public Task<Unit> Handle(CreateDoctorCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
