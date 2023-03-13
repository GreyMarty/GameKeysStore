using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class PlatformValidator : AbstractValidator<PlatformDto>
{
    public PlatformValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);
    }
}