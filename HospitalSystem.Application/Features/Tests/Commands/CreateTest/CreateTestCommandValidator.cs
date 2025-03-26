using FluentValidation;

namespace HospitalSystem.Application.Features.Tests.Commands.CreateTest
{
    public class CreateTestCommandValidator : AbstractValidator<CreateTestCommandRequest>
    {
        public CreateTestCommandValidator()
        {
            RuleFor(x => x.TestName)
                .NotEmpty().WithMessage("Test name is required.")
                .MaximumLength(100).WithMessage("Test name cannot exceed 100 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(50).WithMessage("User name cannot exceed 50 characters.");
        }
    }
}
