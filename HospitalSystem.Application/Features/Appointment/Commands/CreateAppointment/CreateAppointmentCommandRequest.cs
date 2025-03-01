﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Commands.CreateAppointment
{
    public sealed class CreateAppointmentCommandRequest : IRequest<Unit>
    {
    }
}
