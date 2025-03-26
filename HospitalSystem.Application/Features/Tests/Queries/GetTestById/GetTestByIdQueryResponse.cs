using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestById
{
    public class GetTestByIdQueryResponse
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public double? TestPrice { get; set; }
        public string? RefDoctor { get; set; }
        public bool IsReady { get; set; }
        public string UserName { get; set; }
        public TestResultResponse? TestResult { get; set; }

        public GetTestByIdQueryResponse() { }
    }

    public class TestResultResponse
    {
        public List<TestNameAndResultEntryResponse>? TestNameAndResultEntries { get; set; }
        public List<string>? TestImageUrls { get; set; }
        public string? TestConclusion { get; set; }

        public TestResultResponse() { }
    }

    public class TestNameAndResultEntryResponse
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public TestNameAndResultEntryResponse() { }
    }
}
