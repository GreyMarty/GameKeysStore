using Application.Models.WriteModels;
using Application.Validation;
using FluentValidation;

namespace Application.Validation;

public class KeysValidator : AbstractValidator<KeysWriteModel>
{
    public KeysValidator()
    {
        RuleFor(x => x.KeyStrings)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationMessages.MustNotBeNegative);

        RuleFor(x => x.Platform)
            .SetValidator(new PlatformValidator());

        RuleFor(x => x.GameName)
            .NotNull()
            .NotEmpty()
            .Length(4, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);
    }
}