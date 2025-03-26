using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTest
{
    public record CreateTestCommandRequest(
        string TestName,
        double? TestPrice,
        string? RefDoctor,
        string UserName
    ) : IRequest<CreateTestCommandResponse>;
}
