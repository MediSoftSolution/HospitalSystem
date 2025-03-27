using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.UpdateTestTemplate
{
    public record UpdateTestTemplateCommandResponse(int TestTemplateId, bool Success, string Message);
}
