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
        public List<TestNameAndResultEntryResponse>? TestNameAndResultEntries { get; set; }
        public List<string>? TestImageUrls { get; set; }
        public string? TestConclusion { get; set; }
    }
    public class TestNameAndResultEntryResponse
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
