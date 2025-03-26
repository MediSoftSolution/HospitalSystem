using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Commands.DeleteAppointment
{
    public record DeleteAppointmentCommandRequest(int AppointmentId) : IRequest<Unit>;
}
