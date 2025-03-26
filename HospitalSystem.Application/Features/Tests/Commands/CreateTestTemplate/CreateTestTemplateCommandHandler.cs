using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTestTemplate
{
    public class CreateTestTemplateCommandHandler : BaseHandler, IRequestHandler<CreateTestTemplateCommandRequest, CreateTestTemplateCommandResponse>
    {
        public CreateTestTemplateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateTestTemplateCommandResponse> Handle(CreateTestTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            var testTemplate = new Test
            {
                TestName = request.TestName,
                TestPrice = request.TestPrice,
                TestResult = new TestResult
                {
                    TestNameAndResultEntry = request.TestNameAndResultEntry
                      .ConvertAll(entry => new TestNameAndResultEntry { Key = entry.Key, Value = "" })
                }
            };

            await unitOfWork.GetWriteRepository<Test>().AddAsync(testTemplate);
            await unitOfWork.SaveAsync();

            return new CreateTestTemplateCommandResponse(testTemplate.Id, "Test template created successfully.");

        }
    }
}
