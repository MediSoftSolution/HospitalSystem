using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQueryRequest, GetTestByIdQueryResponse?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTestByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetTestByIdQueryResponse?> Handle(GetTestByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.GetReadRepository<Test>().GetAsync(t => t.Id == request.TestId);

            if (test == null) return null;

            return new GetTestByIdQueryResponse
            {
                TestId = test.Id,
                TestName = test.TestName,
                TestPrice = test.TestPrice,
                RefDoctor = test.RefDoctor,
                IsReady = test.IsReady,
                UserName = test.UserName,
                TestResult = test.TestResult != null
                    ? new TestResultResponse
                    {
                        TestNameAndResultEntries = test.TestResult.TestNameAndResultEntry?.Select(entry =>
                            new TestNameAndResultEntryResponse
                            {
                                Key = entry.Key,
                                Value = entry.Value
                            }).ToList() ?? new List<TestNameAndResultEntryResponse>(),
                        TestImageUrls = test.TestResult.TestImageUrl ?? new List<string>(),
                        TestConclusion = test.TestResult.TestConclusion
                    }
                    : null
            };
        }

    }
}
