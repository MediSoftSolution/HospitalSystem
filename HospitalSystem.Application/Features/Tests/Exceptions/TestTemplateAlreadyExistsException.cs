namespace HospitalSystem.Application.Features.Tests.Exceptions
{
    public class TestTemplateAlreadyExistsException : ApplicationException
    {
        public TestTemplateAlreadyExistsException(string testName)
            : base($"Test template '{testName}' already exists.")
        {
        }
    }
}
