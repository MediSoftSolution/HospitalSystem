using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Tests.Commands.CreateTestTemplate;
using HospitalSystem.Application.Features.Tests.Commands.UpdateTestTemplate;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.UpdateTestTemplate
{
    public class UpdateTestTemplateCommandHandler : BaseHandler, IRequestHandler<UpdateTestTemplateCommandRequest, UpdateTestTemplateCommandResponse>
    {
        public UpdateTestTemplateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateTestTemplateCommandResponse> Handle(UpdateTestTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            var testTemplate = await unitOfWork.GetReadRepository<Test>()
                 .GetAsync(t => t.Id == request.TestId, 
                 include: q => q.Include(t => t.TestNameAndResultEntry));

            if (testTemplate == null)
            {
                throw new NotFoundException($"Test template was not found.");
            }

            testTemplate.TestName = request.TestName;
            testTemplate.TestPrice = request.TestPrice;

            var existingEntries = testTemplate.TestNameAndResultEntry
                .ToDictionary(e => e.Key, e => e);

            foreach (var entry in request.TestNameAndResultEntry)
            {
                testTemplate.TestNameAndResultEntry.Add(new TestNameAndResultEntry
                {
                    Key = entry.Key,
                });
            }

            var keysToRemove = existingEntries.Keys.Except(request.TestNameAndResultEntry.Select(e => e.Key)).ToList();
            testTemplate.TestNameAndResultEntry.RemoveAll(e => keysToRemove.Contains(e.Key));

            await unitOfWork.SaveAsync();

            return new UpdateTestTemplateCommandResponse(testTemplate.Id, true, "Test template updated successfully.");

        }
    }
}
