using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Offices.Commands.DeleteOffice
{
    public class DeleteOfficeCommandValidator : AbstractValidator<DeleteOfficeCommandRequest>
    {
        public DeleteOfficeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
