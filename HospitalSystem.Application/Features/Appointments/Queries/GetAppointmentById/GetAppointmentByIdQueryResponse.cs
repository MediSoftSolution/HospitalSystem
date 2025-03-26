using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentById
{
    public record GetAppointmentByIdQueryResponse(
        int AppointmentId,
        Guid PatientId,
        int DoctorId,
        string DoctorName,
        DateTime AppointmentDate
    );

}
