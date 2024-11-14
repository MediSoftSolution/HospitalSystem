using HospitalSystem.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldNotBeExpiredException : BaseException
    {
        public RefreshTokenShouldNotBeExpiredException() : base("Your session has expired. Please log in again.") { }
    }
}
