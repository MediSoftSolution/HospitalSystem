using MediatR;

namespace HospitalSystem.Application.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
