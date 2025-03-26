using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestsByPatient
{
    public record GetTestsByPatientQueryRequest(string PatientName) : IRequest<List<GetTestsByPatientQueryResponse>>;

}
