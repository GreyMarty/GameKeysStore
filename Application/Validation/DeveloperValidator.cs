using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class DeveloperValidator : AbstractValidator<DeveloperWriteModel>
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