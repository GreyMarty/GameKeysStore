using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class SystemRequirementsValidator : AbstractValidator<SytemRequirementsWriteModel>
{
    public SystemRequirementsValidator()
    {
        RuleFor(x => x.OperatingSystem)
            .Length(3, 128)
            .WithName("Operating System")
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Graphics)
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Memory)
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Storage)
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);

        RuleFor(x => x.Processor)
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidSystemRequirementName)
            .WithMessage(ValidationMessages.MustBeValidSystemRequirementName);
    }
}