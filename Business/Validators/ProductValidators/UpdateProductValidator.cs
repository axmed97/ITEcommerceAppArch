using Entities.DTOs.ProductsDTOs;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Business.Validators.ProductValidators;

public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
{
    public UpdateProductValidator()
    {
        RuleFor(x=> x.Name).NotEmpty().NotNull();
        RuleFor(x => x.Count).Must(x => isDigit(x)).WithMessage("Ancaq Reqem");
    }

    private bool isDigit(int value)
    {
        if (Regex.IsMatch(value.ToString(), "^[0-9]+$"))
            return true;

        return false;
    }
}
