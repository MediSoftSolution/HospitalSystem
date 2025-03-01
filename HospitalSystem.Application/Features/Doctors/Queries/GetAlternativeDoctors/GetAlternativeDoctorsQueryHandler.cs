using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAlternativeDoctors
{
    public class GetAlternativeDoctorsQueryHandler : IRequestHandler<GetAlternativeDoctorsQueryRequest, ICollection<GetAlternativeDoctorsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAlternativeDoctorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetAlternativeDoctorsQueryResponse>> Handle(GetAlternativeDoctorsQueryRequest request, CancellationToken cancellationToken)
        {
            var doctorsInSpecialty = await _unitOfWork
                .GetReadRepository<Doctor>()
                .GetAllAsync(d => d.Specialty.Name == request.SpecialityName, include: d => d.Include(d => d.Appointments));

            var availableDoctors = new List<GetAlternativeDoctorsQueryResponse>();

            if (doctorsInSpecialty == null || !doctorsInSpecialty.Any())
            {
                return availableDoctors;
            }

            foreach (var doctor in doctorsInSpecialty)
            {
                bool isAvailable = !doctor.Appointments.Any(d => d.ConsultingDate == request.DateTime);

                if (isAvailable && doctor.User != null)
                {
                    availableDoctors.Add(new GetAlternativeDoctorsQueryResponse
                    {
                        FullName = doctor.User.Fullname,
                        SpecialityName = request.SpecialityName,
                        DateTime = request.DateTime
                    });
                }
            }

            return availableDoctors;
        }
    }
}
