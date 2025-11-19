using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HospitalSystem.Application.Features.Appointment.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : BaseHandler, IRequestHandler<DeleteAppointmentCommandRequest, bool>
    {
        public DeleteAppointmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<bool> Handle(DeleteAppointmentCommandRequest request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.GetReadRepository<Domain.Entities.Appointment>()
                .GetAsync(a => a.Id == request.AppointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            appointment.IsDeleted = false;

            await unitOfWork.GetWriteRepository<Domain.Entities.Appointment>().UpdateAsync(appointment);
            return true;
        }
    }
}