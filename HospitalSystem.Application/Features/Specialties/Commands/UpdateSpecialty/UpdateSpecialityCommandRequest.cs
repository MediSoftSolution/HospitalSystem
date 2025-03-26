using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty
{ 
    public record UpdateSpecialityCommandRequest(int Id, string Name, List<int> DoctorIds, int PhotoId) : IRequest<Unit>;
}
