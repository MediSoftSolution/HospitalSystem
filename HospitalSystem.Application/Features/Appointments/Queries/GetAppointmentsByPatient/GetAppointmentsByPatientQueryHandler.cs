using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentsByPatient
{
    public class GetAppointmentsByPatientQueryHandler : IRequestHandler<GetAppointmentsByPatientQueryRequest, ICollection<GetAppointmentsByPatientQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAppointmentsByPatientQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetAppointmentsByPatientQueryResponse>> Handle(GetAppointmentsByPatientQueryRequest request, CancellationToken cancellationToken)
        {
            var appointments = await _unitOfWork.GetReadRepository<Domain.Entities.Appointment>()
                .GetAllAsync(a => a.PatientId == request.PatientId);

            if (appointments == null || !appointments.Any())
            {
                return new List<GetAppointmentsByPatientQueryResponse>();
            }

            return appointments.Select(appointment => new GetAppointmentsByPatientQueryResponse(
                appointment.Id,
                appointment.Doctor.Id,
                appointment.Doctor.Fullname,
                appointment.ConsultingDate
            )).ToList();
        }

    }
}
