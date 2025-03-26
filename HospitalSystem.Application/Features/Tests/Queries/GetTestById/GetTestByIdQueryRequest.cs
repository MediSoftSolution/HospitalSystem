using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestById
{
    public record GetTestByIdQueryRequest(int TestId) : IRequest<GetTestByIdQueryResponse?>;

}
