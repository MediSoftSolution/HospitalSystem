using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentById
{
    public record GetAppointmentByIdQueryRequest(int Id) : IRequest<GetAppointmentByIdQueryResponse>;
}
