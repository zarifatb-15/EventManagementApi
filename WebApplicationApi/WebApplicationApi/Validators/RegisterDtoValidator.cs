using FluentValidation;
using WebApplicationApi.Dtos.UserDtos;

namespace WebApplicationApi.Validators;

public class RegisterDtoValidator:AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is not empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is not empty")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
    }
}