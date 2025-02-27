using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Auth.Commands.RevokeUserAccessToken
{
    public class RevokeAccessTokenCommandRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
