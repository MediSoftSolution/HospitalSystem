using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
