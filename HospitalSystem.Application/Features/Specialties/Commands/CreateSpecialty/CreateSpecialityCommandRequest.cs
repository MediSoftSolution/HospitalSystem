using MediatR;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Application.Features.Specialties.Commands.CreateSpecialty;

namespace HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality
{
    public record CreateSpecialityCommandRequest(
        string Name,
        int PhotoId
    ) : IRequest<CreateSpecialityCommandResponse>;
}
