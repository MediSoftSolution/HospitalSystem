using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler
    : IRequestHandler<GetAppointmentByIdQueryRequest, GetAppointmentByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAppointmentByIdQueryResponse> Handle(GetAppointmentByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.GetReadRepository<Domain.Entities.Appointment>()
                .GetAsync(a => a.Id == request.Id);

            if (appointment == null)
            {
                return null;
            }

            return new GetAppointmentByIdQueryResponse(
                appointment.Id,
                appointment.PatientId,
                appointment.Doctor.Id,
                appointment.Doctor.Fullname,
                appointment.ConsultingDate
            );
        }
    }
}
