using HospitalSystem.Application.Bases;

namespace HospitalSystem.Application.Features.Doctors.Exceptions
{
    public class DoctorBusinessException : BaseException
    {
        public DoctorBusinessException(string message) : base(message) { }

    }
}
