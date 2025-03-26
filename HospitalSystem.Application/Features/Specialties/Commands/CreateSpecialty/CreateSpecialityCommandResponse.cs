namespace HospitalSystem.Application.Features.Specialties.Commands.CreateSpecialty
{
    public record CreateSpecialityCommandResponse(
        int Id,
        bool Success,
        string Message
    );
}
