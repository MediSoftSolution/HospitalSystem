using MediatR;

namespace HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality
{
    public record DeleteSpecialityCommandRequest(int Id) : IRequest<bool>;
}
