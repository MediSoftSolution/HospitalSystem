using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.UpdateTest
{
    public record UpdateTestCommandRequest(
        int Id,
        string? RefDoctor,
        bool IsReady,
        string UserName,
        TestResultRequest? TestResult
    ) : IRequest<UpdateTestCommandResponse>;

    public record TestResultRequest(
        List<TestNameAndResultEntryRequest>? TestNameAndResultEntries,
        List<string>? TestImageUrls,
        string? TestConclusion
    );

    public record TestNameAndResultEntryRequest(
        string Value
    );

}
