using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandHandler : BaseHandler, IRequestHandler<CreateTestCommandRequest, CreateTestCommandResponse>
    {
        public CreateTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateTestCommandResponse> Handle(CreateTestCommandRequest request, CancellationToken cancellationToken)
        {
            var testTemplate = await unitOfWork.GetReadRepository<TestTemplate>().GetAsync(x => x.TestName == request.TestName);
            if (testTemplate == null)
                return new CreateTestCommandResponse(false, "Test template not found.");

            var patientTest = new Test
            {
                TestName = request.TestName,
                UserName = request.UserName,
                TestPrice = testTemplate.TestPrice,
                RefDoctor = request.RefDoctor,
                IsReady = false,
                TestNameAndResultEntry = testTemplate.TestKeys
                    .Select(key => new TestNameAndResultEntry { Key = key.Key, Value = "" })
                    .ToList()
            };


            await unitOfWork.GetWriteRepository<Test>().AddAsync(patientTest);
            await unitOfWork.SaveAsync();

            return new CreateTestCommandResponse(true, "Patient test created successfully.");

        }
    }
}


