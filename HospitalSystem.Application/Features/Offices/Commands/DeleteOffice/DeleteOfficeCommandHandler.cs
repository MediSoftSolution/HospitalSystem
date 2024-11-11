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

namespace HospitalSystem.Application.Features.Offices.Commands.DeleteOffice
{
    public class DeleteOfficeCommandHandler : BaseHandler, IRequestHandler<DeleteOfficeCommandRequest, Unit>
    {
        public DeleteOfficeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteOfficeCommandRequest request, CancellationToken cancellationToken)
        {
            var office = await unitOfWork.GetReadRepository<Office>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            office.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Office>().UpdateAsync(office);
            await unitOfWork.SaveAsync();

            return Unit.Value;        
        }
    }
}
