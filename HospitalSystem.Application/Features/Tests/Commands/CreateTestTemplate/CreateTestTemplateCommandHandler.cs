using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Tests.Constants;
using HospitalSystem.Application.Features.Tests.Exceptions;
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
            var existing = await unitOfWork.GetReadRepository<TestTemplate>()
                .GetAsync(x => x.TestName.ToLower().Trim() == request.TestName.ToLower().Trim());

            if (existing != null)
                throw new TestTemplateAlreadyExistsException(request.TestName);

            var template = new TestTemplate
            {
                TestName = request.TestName,
                TestPrice = request.TestPrice,
                Keys = new List<TestTemplateKey>()
            };

            if (request.TestNameAndResultEntry != null)
            {
                foreach (var entry in request.TestNameAndResultEntry)
                {
                    template.Keys.Add(new TestTemplateKey
                    {
                        Key = entry.Key,
                        TestTemplate = template
                    });
                }
            }

            await unitOfWork.GetWriteRepository<TestTemplate>().AddAsync(template);
            await unitOfWork.SaveAsync();

            return new CreateTestTemplateCommandResponse(true, TestMessages.TemplateCreatedSuccessfully);
        }

    }
}
