using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty
{
    public class UpdateSpecialityCommandValidator : AbstractValidator<UpdateSpecialityCommandRequest>
    {
        public UpdateSpecialityCommandValidator()
        {
           
        }
    }
}
