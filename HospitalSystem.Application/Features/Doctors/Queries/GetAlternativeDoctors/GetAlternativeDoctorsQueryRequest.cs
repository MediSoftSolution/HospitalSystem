using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Doctors.Queries.GetAlternativeDoctors
{
    public class GetAlternativeDoctorsQueryRequest : IRequest<ICollection<GetAlternativeDoctorsQueryResponse>>
    {
        public string SpecialityName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
