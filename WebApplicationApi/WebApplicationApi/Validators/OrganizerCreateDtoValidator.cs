using FluentValidation;
using WebApplicationApi.Dtos.OrganizerDtos;

namespace WebApplicationApi.Validators;

public class OrganizerCreateDtoValidator: AbstractValidator<OrganizerCreateDto>
{
    public OrganizerCreateDtoValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Organizer name cannot be empty.")
            .MaximumLength(100).WithMessage("Organizer name cannot exceed 100 characters.");

        RuleFor(o => o.Email)
            .NotEmpty().WithMessage("Email address cannot be empty.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(o => o.Phone)
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");
    }
}