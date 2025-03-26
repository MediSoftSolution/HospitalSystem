using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommandHandler : BaseHandler, IRequestHandler<DeleteTestCommandRequest, Unit>
    {
        public DeleteTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteTestCommandRequest request, CancellationToken cancellationToken)
        {
            var test = await unitOfWork.GetReadRepository<Test>().GetAsync(t => t.Id == request.Id);

            if (test == null)
                throw new Exception("Test not found.");

            test.IsDeleted = true;
            await unitOfWork.GetWriteRepository<Test>().UpdateAsync(test);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
