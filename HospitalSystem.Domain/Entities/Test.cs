using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Test : EntityBase
    { 
        public string TestName { get; set; }
        public double? TestPrice { get; set; }
        public string? RefDoctor { get; set; }
        public TestResult? TestResult { get; set; }
        public bool IsReady { get; set; } = false;
        public string UserName { get; set; }
    }
public class TestResult : EntityBase
    { 
        public List<TestNameAndResultEntry>? TestNameAndResultEntry { get; set; }
        public List<string>? TestImageUrl { get; set; }
        public string? TestConclusion { get; set; }
        public TestResult()
        {
            TestImageUrl = new List<string>();
            TestNameAndResultEntry = new List<TestNameAndResultEntry>();
        }
    }
public class TestNameAndResultEntry : EntityBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int TestResultId { get; set; }
        public TestResult TestResult { get; set; }
    }
