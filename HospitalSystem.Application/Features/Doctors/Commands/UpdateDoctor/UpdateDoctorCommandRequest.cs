using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor
{ 
    public class UpdateDoctorCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? About { get; set; }
        public Dictionary<DayOfWeek, WorkingTime> WorkingTimes { get; set; }
        public decimal ConsultingFee { get; set; }
        public int PhotoId { get; set; }
        public int? SpecialtyId { get; set; }
        public int? OfficeId { get; set; }
    }
}
