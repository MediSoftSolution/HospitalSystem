using MediatR;

namespace HospitalSystem.Application.Features.Tests.Commands.DeleteTest
{
    public record DeleteTestCommandRequest(int Id) : IRequest<bool>;

}
