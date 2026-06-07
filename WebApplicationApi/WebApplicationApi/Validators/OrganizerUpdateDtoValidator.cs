using FluentValidation;
using WebApplicationApi.Dtos.OrganizerDtos;

namespace WebApplicationApi.Validators;

public class OrganizerUpdateDtoValidator : AbstractValidator<OrganizerUpdateDto>
{
    public OrganizerUpdateDtoValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        RuleFor(o => o.ContactEmail)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
        RuleFor(o => o.ContactPhone)
            .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.");
    }
}