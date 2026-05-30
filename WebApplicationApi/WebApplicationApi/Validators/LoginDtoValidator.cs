using FluentValidation;
using WebApplicationApi.Dtos.UserDtos;

namespace WebApplicationApi.Validators;

public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is not empty.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is not empty.");
    }
}