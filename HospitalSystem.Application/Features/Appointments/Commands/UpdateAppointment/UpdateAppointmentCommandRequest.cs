using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Commands.UpdateAppointment
{
    public record UpdateAppointmentCommandRequest(
        int AppointmentId,
        DateTime NewAppointmentDate,
        string Message
    ) : IRequest<UpdateAppointmentCommandResponse>;

}
