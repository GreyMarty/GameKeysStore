using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class SystemRequirementsValidator : AbstractValidator<SystemRequirementsWriteModel>
{
    public SystemRequirementsValidator()
    {
        RuleFor(x => x.OperatingSystem)
            .NotEmpty()
            .Length(3, 128)
            .WithName("Operating System")
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Graphics)
            .NotEmpty()
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Memory)
            .NotEmpty()
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Storage)
            .NotEmpty()
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Processor)
            .NotEmpty()
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);
    }
}