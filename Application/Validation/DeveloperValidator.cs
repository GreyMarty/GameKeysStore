using Application.Models;
using FluentValidation;

namespace Application.Validation;

public class DeveloperValidator : AbstractValidator<DeveloperDto>
{
    public DeveloperValidator()
    {
        RuleFor(x => x.Name)
            .Length(4, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);
    }
}