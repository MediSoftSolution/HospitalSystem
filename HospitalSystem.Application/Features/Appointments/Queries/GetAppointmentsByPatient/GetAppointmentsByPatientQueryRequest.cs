using HospitalSystem.Application.Features.Doctors.Queries.GetAlternativeDoctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentsByPatient
{
    public record GetAppointmentsByPatientQueryRequest(Guid PatientId) : IRequest<ICollection<GetAppointmentsByPatientQueryResponse>>;
}
