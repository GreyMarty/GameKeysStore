using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class PlatformValidator : AbstractValidator<PlatformWriteModel>
{
    public PlatformValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);
    }
}