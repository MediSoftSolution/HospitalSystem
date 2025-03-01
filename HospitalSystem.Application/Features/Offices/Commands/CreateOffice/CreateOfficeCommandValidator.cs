using FluentValidation;

namespace HospitalSystem.Application.Features.Offices.Commands.CreateOffice
{
    public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommandRequest>
    {
        public CreateOfficeCommandValidator()
        {
        }
        
    }
}
