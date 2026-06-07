using FluentValidation;
using WebApplicationApi.Dtos.TicketDtos;

namespace WebApplicationApi.Validators;

public class TicketUpdateDtoValidator : AbstractValidator<TicketUpdateDto>
{
    public TicketUpdateDtoValidator()
    {
        RuleFor(t => t.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(50).WithMessage("Type cannot exceed 50 characters.");
        RuleFor(t => t.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");
        RuleFor(t => t.QuantityAvailable)
            .GreaterThanOrEqualTo(0).WithMessage("QuantityAvailable must be a non-negative integer.");
    }
}