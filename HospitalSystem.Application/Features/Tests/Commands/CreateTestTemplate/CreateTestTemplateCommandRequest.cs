using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTestTemplate
{
    public record CreateTestTemplateCommandRequest(
        string TestName,
        double TestPrice,
        List<TestParameterEntry> TestNameAndResultEntry
    ) : IRequest<CreateTestTemplateCommandResponse>;

    public record TestParameterEntry(string Key);
}
