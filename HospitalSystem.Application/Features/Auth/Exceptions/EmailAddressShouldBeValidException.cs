using HospitalSystem.Application.Bases;

namespace HospitalSystem.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldBeValidException : BaseException
    {
        public EmailAddressShouldBeValidException() : base("There is no such email address.") { }
    }
}
