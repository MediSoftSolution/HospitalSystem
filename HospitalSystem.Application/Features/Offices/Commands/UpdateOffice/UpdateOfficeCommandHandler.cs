using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Domain.Entities;
using AutoMapper;

namespace HospitalSystem.Application.Features.Offices.Commands.UpdateDoctor
{
    public class UpdateOfficeCommandHandler : BaseHandler, IRequestHandler<UpdateOfficeCommandRequest, bool>
    {
        public UpdateOfficeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<bool> Handle(UpdateOfficeCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Office>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            var map = mapper.Map<Office>(request);

            await unitOfWork.GetWriteRepository<Office>().UpdateAsync(map);
            await unitOfWork.SaveAsync();

            return true;
        }
    }
}
