using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAlternativeDoctors
{
    public class GetAlternativeDoctorsQueryResponse
    {
        public Guid DoctorId { get; set; }
        public string FullName { get; set; }
        public string SpecialityName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
