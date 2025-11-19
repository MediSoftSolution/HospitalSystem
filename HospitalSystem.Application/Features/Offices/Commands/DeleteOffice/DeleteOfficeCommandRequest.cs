using MediatR;

namespace HospitalSystem.Application.Features.Offices.Commands.DeleteOffice
{
    public class DeleteOfficeCommandRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
