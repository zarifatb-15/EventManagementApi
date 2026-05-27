using FluentValidation;
using WebApplicationApi.Dtos.TicketDtos;


namespace WebApplicationApi.Validators;

public class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
{
    public TicketCreateDtoValidator()
    {
        RuleFor(t => t.Type)
            .NotEmpty().WithMessage("Ticket type cannot be empty.")
            .MaximumLength(50).WithMessage("Ticket type cannot exceed 50 characters.");

        RuleFor(t => t.Price)
            .GreaterThan(0).WithMessage("Ticket price must be a positive number greater than 0.");

        RuleFor(t => t.QuantityAvailable)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity available cannot be negative.");

        RuleFor(t => t.EventId)
            .GreaterThan(0).WithMessage("A valid EventId must be provided.");
    }
}