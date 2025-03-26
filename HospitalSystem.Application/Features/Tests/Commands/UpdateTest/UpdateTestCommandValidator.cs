
using FluentValidation;

namespace HospitalSystem.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandValidator : AbstractValidator<UpdateTestCommandRequest>
    {
        public UpdateTestCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Test ID must be greater than 0.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.");

            RuleForEach(x => x.TestResult.TestNameAndResultEntries)
                .SetValidator(new TestNameAndResultEntryValidator())
                .When(x => x.TestResult?.TestNameAndResultEntries != null);
        }
    }

    public class TestNameAndResultEntryValidator : AbstractValidator<TestNameAndResultEntryRequest>
    {
        public TestNameAndResultEntryValidator()
        {

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Result value cannot be empty.");
        }
    }

}
