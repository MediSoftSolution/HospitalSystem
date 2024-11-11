using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
