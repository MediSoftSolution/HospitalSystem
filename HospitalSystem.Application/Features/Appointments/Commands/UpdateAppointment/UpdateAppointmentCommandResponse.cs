namespace HospitalSystem.Application.Features.Appointment.Commands.UpdateAppointment
{
    public record UpdateAppointmentCommandResponse(
        int AppointmentId,
        bool Success,
        string Message
    );

}
