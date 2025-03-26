using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Tests.Commands.CreateTest;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandHandler : BaseHandler, IRequestHandler<CreateTestCommandRequest, CreateTestCommandResponse>
    {
        public CreateTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateTestCommandResponse> Handle(CreateTestCommandRequest request, CancellationToken cancellationToken)
        {
            Test test = new Test
            {
                TestName = request.TestName,
                TestPrice = request.TestPrice,
                RefDoctor = request.RefDoctor,
                IsReady = false,
                UserName = request.UserName
            };

            await unitOfWork.GetWriteRepository<Test>().AddAsync(test);
            await unitOfWork.SaveAsync();

            return new CreateTestCommandResponse(test.Id, test.TestName, test.TestPrice, 
                test.RefDoctor, test.IsReady);
        }
    }
}


