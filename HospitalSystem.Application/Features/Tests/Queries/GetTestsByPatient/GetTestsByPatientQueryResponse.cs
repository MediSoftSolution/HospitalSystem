using HospitalSystem.Application.Features.Tests.Queries.GetTestById;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestsByPatient
{
    public record GetTestsByPatientQueryResponse(
        int TestId,
        string TestName,
        string? RefDoctor,
        bool IsReady,
        string UserName
    );
}
