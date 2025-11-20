using MediatR;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTest
{
    public record CreateTestCommandRequest(
        Guid PatientId,
        string TestName,
        string? RefDoctor,
        string UserName
    ) : IRequest<CreateTestCommandResponse>;
}
