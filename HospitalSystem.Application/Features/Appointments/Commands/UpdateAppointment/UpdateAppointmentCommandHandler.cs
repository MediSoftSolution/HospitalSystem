using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : BaseHandler, IRequestHandler<UpdateAppointmentCommandRequest, UpdateAppointmentCommandResponse>
    {
        public UpdateAppointmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateAppointmentCommandResponse> Handle(UpdateAppointmentCommandRequest request, CancellationToken cancellationToken)
        {
            var appointment = await unitOfWork.GetReadRepository<Domain.Entities.Appointment>().GetAsync(a => a.Id == request.AppointmentId);

            if (appointment == null)
            {
                return new UpdateAppointmentCommandResponse(request.AppointmentId, false, "Appointment not found.");
            }

            if (appointment.ConsultingDate < DateTime.UtcNow)
            {
                return new UpdateAppointmentCommandResponse(request.AppointmentId, false, "Cannot update past appointments.");
            }

            appointment.ConsultingDate = request.NewAppointmentDate;
            appointment.Message = request.Message;
            await unitOfWork.GetWriteRepository<Domain.Entities.Appointment>().UpdateAsync(appointment);

            return new UpdateAppointmentCommandResponse(request.AppointmentId, true, "Appointment updated successfully.");
        }
    }
}
