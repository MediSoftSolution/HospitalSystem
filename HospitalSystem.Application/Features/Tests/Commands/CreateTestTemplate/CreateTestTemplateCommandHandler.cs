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
            var template = await unitOfWork.GetReadRepository<TestTemplate>()
                .GetAsync(tt => tt.TestName == request.TestName);

            if (template != null)
            {
                throw new Exception("Test template already existed.");
            }

            var testTemplate = new TestTemplate
            {
                TestName = request.TestName,
                TestPrice = request.TestPrice,
                TestKeys = request.TestNameAndResultEntry?.Select( entry => 
                new TestTemplateKey { Key = entry.Key }).ToList()
            };


            await unitOfWork.GetWriteRepository<TestTemplate>().AddAsync(testTemplate);
            await unitOfWork.SaveAsync();

            return new CreateTestTemplateCommandResponse(true, "Test template added successfully!");

        }
    }
}
