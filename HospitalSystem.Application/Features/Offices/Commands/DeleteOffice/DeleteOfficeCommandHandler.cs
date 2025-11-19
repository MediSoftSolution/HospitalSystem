using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using HospitalSystem.Domain.Entities;
using AutoMapper;

namespace HospitalSystem.Application.Features.Offices.Commands.DeleteOffice
{
    public class DeleteOfficeCommandHandler : BaseHandler, IRequestHandler<DeleteOfficeCommandRequest, bool>
    {
        public DeleteOfficeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<bool> Handle(DeleteOfficeCommandRequest request, CancellationToken cancellationToken)
        {
            var office = await unitOfWork.GetReadRepository<Office>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            office.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Office>().UpdateAsync(office);
            await unitOfWork.SaveAsync();

            return true;        
        }
    }
}
