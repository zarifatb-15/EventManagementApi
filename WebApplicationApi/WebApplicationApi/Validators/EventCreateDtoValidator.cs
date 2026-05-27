using FluentValidation;
using WebApplicationApi.Dtos.EventDtos;

namespace WebApplicationApi.Validators;

public class EventCreateDtoValidator: AbstractValidator<EventCreateDto>
{
    public EventCreateDtoValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage("Event title cannot be empty.")
            .MaximumLength(150).WithMessage("Event title cannot exceed 150 characters.");

        RuleFor(e => e.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("Location cannot be empty.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");

        RuleFor(e => e.Date)
            .NotEmpty().WithMessage("Event date is required.")
            .GreaterThan(DateTime.Now).WithMessage("Event date must be a future date.");

        RuleFor(e => e.OrganizerId)
            .GreaterThan(0).WithMessage("A valid OrganizerId must be provided.");
    }
}