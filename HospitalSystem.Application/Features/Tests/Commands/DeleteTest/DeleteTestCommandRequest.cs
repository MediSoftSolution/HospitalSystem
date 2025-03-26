﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.DeleteTest
{
    public record DeleteTestCommandRequest(int Id) : IRequest<Unit>;

}
