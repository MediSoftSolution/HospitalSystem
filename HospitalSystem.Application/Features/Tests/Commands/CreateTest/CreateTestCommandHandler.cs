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
            var testTemplate = await unitOfWork.GetReadRepository<Test>().GetAsync(x => x.TestName == request.TestName);
            if (testTemplate == null)
                return new CreateTestCommandResponse(0, "Test template not found.");

            var patientTest = new Test
            {
                UserName = request.UserName,
                RefDoctor = request.RefDoctor,
                TestName = request.TestName,
                IsReady = false,
                TestResult = new TestResult
                {
                    TestNameAndResultEntry = testTemplate.TestResult.TestNameAndResultEntry
                        .Select(entry => new TestNameAndResultEntry { Key = entry.Key, Value = "" }).ToList()
                }
            };

            await unitOfWork.GetWriteRepository<Test>().AddAsync(patientTest);
            await unitOfWork.SaveAsync();

            return new CreateTestCommandResponse(patientTest.Id, "Patient test created successfully.");

        }
    }
}


