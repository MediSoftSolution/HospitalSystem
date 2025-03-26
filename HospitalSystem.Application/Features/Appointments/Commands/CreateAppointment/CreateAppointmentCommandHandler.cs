using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Appointment.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : BaseHandler, IRequestHandler<CreateAppointmentCommandRequest, CreateAppointmentCommandResponse>
    {
        public CreateAppointmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateAppointmentCommandResponse> Handle(CreateAppointmentCommandRequest request, CancellationToken cancellationToken)
        {
            var patient = unitOfWork.GetReadRepository<User>().Find(u => u.Id == request.PatientId);
            if (patient == null)
            {
                return new CreateAppointmentCommandResponse(int.MinValue, false, "Patient not found.");
            }

            var doctor = await unitOfWork.GetReadRepository<Doctor>().GetAsync(a => a.Id == request.DoctorId);
            if (doctor == null)
            {
                return new CreateAppointmentCommandResponse(int.MinValue, false, "Doctor not found.");
            }

            bool isAvailable = await unitOfWork.GetReadRepository<Domain.Entities.Appointment>()
                .GetAsync(a => a.ConsultingDate == request.AppointmentDate) is null;

            if (!isAvailable) return new CreateAppointmentCommandResponse(int.MinValue, false, "Doctor is not available at the selected time.");

            var appointment = new Domain.Entities.Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                ConsultingDate = request.AppointmentDate,
            };

            await unitOfWork.GetWriteRepository<Domain.Entities.Appointment>().AddAsync(appointment);

            return new CreateAppointmentCommandResponse(appointment.Id, true, "Appointment created successfully.");
        }
    }
}
