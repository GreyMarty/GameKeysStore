using Application.Models;
using FluentValidation;

namespace Application.Validation;

public class GameValidator : AbstractValidator<GameDto>
{
    public GameValidator()
    {
        RuleFor(x => x.Name)
            .Length(4, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(ValidationMessages.MustNotBeEmpty);

        RuleFor(x => x.Developer)
            .SetValidator(new DeveloperValidator());

        RuleFor(x => x.RecommendedSystemRequirements)
            .SetValidator(new SystemRequirementsValidator());
    }
}