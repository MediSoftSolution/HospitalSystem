using HospitalSystem.Application.Bases;

namespace HospitalSystem.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistException : BaseException
    {
        public UserAlreadyExistException() : base("Such a user already exists!") { }
    }
}
