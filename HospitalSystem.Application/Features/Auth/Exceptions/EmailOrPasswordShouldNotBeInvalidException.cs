using HospitalSystem.Application.Bases;

namespace HospitalSystem.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInvalidException : BaseException
    {
        public EmailOrPasswordShouldNotBeInvalidException() : base("The username or password is incorrect.") { }

    }
}
