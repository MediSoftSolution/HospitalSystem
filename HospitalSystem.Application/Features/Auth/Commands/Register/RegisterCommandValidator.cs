using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(x => x.Password);
        }
    }
}
