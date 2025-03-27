using HospitalSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain.Entities
{
    public class TestTemplate : EntityBase
    {
        public string TestName { get; set; }
        public double? TestPrice { get; set; }
        public List<TestTemplateKey>? TestKeys { get; set; } = new List<TestTemplateKey>();
    }

    public class TestTemplateKey : EntityBase
    {
        public string Key { get; set; }
        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }
    }
}
