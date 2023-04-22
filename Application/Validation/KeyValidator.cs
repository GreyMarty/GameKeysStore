using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class KeyValidator : AbstractValidator<KeyWriteModel>
{
    public KeyValidator()
    {
        RuleFor(x => x.KeyString)
            .NotEmpty()
            .WithMessage(ValidationMessages.MustNotBeEmpty);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ValidationMessages.MustNotBeNegative);

        RuleFor(x => x.Platform)
            .SetValidator(new PlatformValidator());
    }
}