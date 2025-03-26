using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestsByPatient
{
    public record GetTestsByPatientQueryResponse(
        int TestId,
        string TestName,
        double? TestPrice,
        string? RefDoctor,
        bool IsReady,
        string UserName,
        TestResultResponse? TestResult
    );

    public record TestResultResponse(
        List<TestNameAndResultEntryResponse>? TestNameAndResultEntries,
        List<string>? TestImageUrls,
        string? TestConclusion
    );

    public record TestNameAndResultEntryResponse(
        string Key,
        string Value
    );

}
