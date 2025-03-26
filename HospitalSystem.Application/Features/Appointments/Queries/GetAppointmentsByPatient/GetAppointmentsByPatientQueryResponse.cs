using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentsByPatient
{
    public record GetAppointmentsByPatientQueryResponse
        (int AppointmentId, int DoctorId, string DoctorName, DateTime AppointmentDate);
}
