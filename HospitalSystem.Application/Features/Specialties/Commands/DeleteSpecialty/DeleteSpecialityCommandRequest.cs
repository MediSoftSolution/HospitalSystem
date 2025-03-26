using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality
{
    public record DeleteSpecialityCommandRequest(int Id) : IRequest<Unit>;
}
