using FluentValidation;
using WebApplicationApi.Dtos.EventDtos; 

namespace WebApplicationApi.Validators; 

public class EventUpdateDtoValidator : AbstractValidator<EventUpdateDto>
{
    public EventUpdateDtoValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage("Title is required.") 
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.")
            .When(e => e.Title != null);
        
        RuleFor(e => e.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(e => e.Description != null);
        
        RuleFor(e => e.Date)
            .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future.")
            .When(e => e.Date.HasValue);
        
        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.")
            .When(e => e.Location != null);
    }
}