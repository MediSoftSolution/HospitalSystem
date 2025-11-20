using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities;

public class Test : EntityBase
{
    public int TestTemplateId { get; set; }
    public TestTemplate TestTemplate { get; set; }

    public Guid PatientId { get; set; }
    public User Patient { get; set; }

    public bool IsReady { get; set; } = false;
    public string? RefDoctor { get; set; }
    public string? TestConclusion { get; set; }

    public List<TestResultEntry> Results { get; set; } = new();
    public List<TestImage> Images { get; set; } = new();
}


public class TestImage : EntityBase
{
    public string ImageUrl { get; set; }

    public int TestId { get; set; }
    public Test Test { get; set; }
}


public class TestResultEntry : EntityBase
{
    public string Key { get; set; }      
    public string Value { get; set; }   

    public int TestId { get; set; }
    public Test Test { get; set; }
}


