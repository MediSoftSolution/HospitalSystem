using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality
{
    public class CreateSpecialityCommandRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
        public int PhotoId { get; set; }
    }
}
