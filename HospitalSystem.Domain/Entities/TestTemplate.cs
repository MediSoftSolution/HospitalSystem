using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities
{
    public class TestTemplate : EntityBase
    {
        public string TestName { get; set; }
        public double TestPrice { get; set; }

        public List<TestTemplateKey> Keys { get; set; } = new();
        public List<Test> Tests { get; set; } = new();
    }


    public class TestTemplateKey : EntityBase
    {
        public string Key { get; set; }
        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }
    }
}
