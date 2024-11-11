using FluentValidation;
using HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Specialities.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommandValidator : AbstractValidator<DeleteSpecialityCommandRequest>
    {
        public DeleteSpecialityCommandValidator()
        {
           
        }
    }
}
