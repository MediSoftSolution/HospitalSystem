using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Test : EntityBase
{
    public string TestName { get; set; }
    public double? TestPrice { get; set; }
    public string? RefDoctor { get; set; }
    public bool IsReady { get; set; } = false;
    public string UserName { get; set; }
    public List<TestImage> TestImages { get; set; } = new List<TestImage>();
    public List<TestNameAndResultEntry>? TestNameAndResultEntry { get; set; } = new List<TestNameAndResultEntry>();
    public string? TestConclusion { get; set; }
}

public class TestImage : EntityBase
{
    public string ImageUrl { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
}

public class TestNameAndResultEntry : EntityBase
{
    public string Key { get; set; }
    public string Value { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
}

