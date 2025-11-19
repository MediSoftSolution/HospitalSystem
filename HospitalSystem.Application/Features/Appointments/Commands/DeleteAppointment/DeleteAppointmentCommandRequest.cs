using MediatR;

namespace HospitalSystem.Application.Features.Appointment.Commands.DeleteAppointment
{
    public record DeleteAppointmentCommandRequest(int AppointmentId) : IRequest<bool>;
}
