using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty
{ 
    public class UpdateSpecialityCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }
        public int PhotoId { get; set; }
    }
}
