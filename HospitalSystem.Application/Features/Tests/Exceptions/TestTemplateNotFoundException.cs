namespace HospitalSystem.Application.Features.Tests.Exceptions
{
    public class TestTemplateNotFoundException : ApplicationException
    {
        public TestTemplateNotFoundException(string testName)
            : base($"Test template '{testName}' not found.")
        {
        }
    }
}
