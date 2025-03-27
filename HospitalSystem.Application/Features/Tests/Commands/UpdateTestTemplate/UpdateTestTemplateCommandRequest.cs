using HospitalSystem.Application.Features.Tests.Commands.CreateTestTemplate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.UpdateTestTemplate
{
    public record UpdateTestTemplateCommandRequest
    (
        int TestId,
        string TestName,
        double TestPrice,
        List<TestParameterEntry> TestNameAndResultEntry
    ): IRequest<UpdateTestTemplateCommandResponse>;
}
