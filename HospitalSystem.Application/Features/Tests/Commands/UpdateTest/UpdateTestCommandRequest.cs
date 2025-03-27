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
        List<TestNameAndResultEntryRequest>? TestNameAndResultEntries,
        List<string>? TestImageUrls,
        string? TestConclusion
    ) : IRequest<UpdateTestCommandResponse>;

    public record TestNameAndResultEntryRequest(
        string Key,
        string Value
    );

}
