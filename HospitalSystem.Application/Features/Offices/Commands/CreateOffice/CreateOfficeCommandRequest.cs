using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Offices.Commands.CreateOffice
{
    public class CreateOfficeCommandRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Tel { get; set; }
        public ICollection<WorkingTime> WorkingTimes { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
