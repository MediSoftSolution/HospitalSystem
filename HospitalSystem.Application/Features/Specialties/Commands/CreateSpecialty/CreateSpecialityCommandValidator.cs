using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality
{
    public class CreateSpecialityCommandValidator : AbstractValidator<CreateSpecialityCommandRequest>
    {
        public CreateSpecialityCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Speciality name is required.")
            .MaximumLength(100).WithMessage("Speciality name cannot exceed 100 characters.");

            RuleFor(x => x.PhotoId)
                .GreaterThan(0).WithMessage("Photo ID must be greater than zero.");
        }
    }
}
