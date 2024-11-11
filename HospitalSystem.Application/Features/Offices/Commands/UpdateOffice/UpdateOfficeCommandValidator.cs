using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Offices.Commands.UpdateDoctor
{
    public class UpdateOfficeCommandValidator : AbstractValidator<UpdateOfficeCommandRequest>
    {
        public UpdateOfficeCommandValidator()
        {
          
        }
        
    }
}
