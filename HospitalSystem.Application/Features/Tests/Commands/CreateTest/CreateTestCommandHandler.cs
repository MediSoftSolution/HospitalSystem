using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Tests.Constants;
using HospitalSystem.Application.Features.Tests.Exceptions;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandHandler : BaseHandler, IRequestHandler<CreateTestCommandRequest, CreateTestCommandResponse>
    {
        public CreateTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateTestCommandResponse> Handle(CreateTestCommandRequest request, CancellationToken cancellationToken)
        {
            var testTemplate = await unitOfWork.GetReadRepository<TestTemplate>()
                .GetAsync(x => x.TestName == request.TestName,
                          include: x => x.Include(d => d.Keys));

            if (testTemplate == null)
                throw new TestTemplateNotFoundException(request.TestName);

            var patientTest = new Test
            {
                TestTemplateId = testTemplate.Id,
                PatientId = request.PatientId,
                RefDoctor = request.RefDoctor,
                IsReady = false,
                Results = testTemplate.Keys
                    .Select(k => new TestResultEntry
                    {
                        Key = k.Key,
                        Value = ""
                    })
                    .ToList()
            };

            await unitOfWork.GetWriteRepository<Test>().AddAsync(patientTest);
            await unitOfWork.SaveAsync();

            return new CreateTestCommandResponse(true, TestMessages.TestCreatedSuccessfully);
        }


    }
}


