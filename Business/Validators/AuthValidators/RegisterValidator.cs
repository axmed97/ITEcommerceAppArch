using Entities.DTOs.AuthDTOs;
using FluentValidation;

namespace Business.Validators.AuthValidators;

public class RegisterValidator : AbstractValidator<RegisterDTO>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Confirm Password is must be equal to Password");

        RuleFor(x => x.Phone).Matches("^[0-9]+$").WithMessage("Ancax ruscax");
    }
}
