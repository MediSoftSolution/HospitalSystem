using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Email.Commands.Send
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommandRequest>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.To).NotEmpty().EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Subject is required.");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Email body cannot be empty.");
        }
    }
}
