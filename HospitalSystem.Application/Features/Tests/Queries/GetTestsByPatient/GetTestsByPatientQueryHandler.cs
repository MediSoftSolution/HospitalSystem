using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestsByPatient
{
    public class GetTestsByPatientQueryHandler : IRequestHandler<GetTestsByPatientQueryRequest, List<GetTestsByPatientQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTestsByPatientQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetTestsByPatientQueryResponse>> Handle(GetTestsByPatientQueryRequest request, CancellationToken cancellationToken)
        {
            var tests = await _unitOfWork.GetReadRepository<Test>().GetAllAsync(t => t.UserName == request.PatientName);

            if (tests == null || !tests.Any())
                return new List<GetTestsByPatientQueryResponse>();

            return tests.Select(test => new GetTestsByPatientQueryResponse(
                test.Id,
                test.TestName,
                test.TestPrice,
                test.RefDoctor,
                test.IsReady,
                test.UserName,
                test.TestResult == null ? null : new TestResultResponse(
                    test.TestResult.TestNameAndResultEntry?
                        .Select(entry => new TestNameAndResultEntryResponse(entry.Key, entry.Value))
                        .ToList(),
                    test.TestResult.TestImageUrl,
                    test.TestResult.TestConclusion
                )
            )).ToList();
        }
    }
}
