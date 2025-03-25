﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalSystem.Domain.Entities;

namespace HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommandRequest>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ConsultingFee)
                .GreaterThan(0).WithMessage("ConsultingFee must be greater than zero.");

            RuleFor(x => x.PhotoId)
                .GreaterThan(0).WithMessage("PhotoId must be greater than zero.");

            RuleFor(x => x.SpecialtyId)
                .GreaterThan(0).When(x => x.SpecialtyId.HasValue).WithMessage("SpecialtyId must be greater than zero.");

            RuleFor(x => x.OfficeId)
                .GreaterThan(0).When(x => x.OfficeId.HasValue).WithMessage("OfficeId must be greater than zero.");

            RuleFor(x => x.WorkingTimes)
                .NotEmpty().WithMessage("WorkingTimes must be provided.");        
        }
       
    }
}
