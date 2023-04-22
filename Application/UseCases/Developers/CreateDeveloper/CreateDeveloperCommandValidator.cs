using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Developers.CreateDeveloper;

internal class CreateDeveloperCommandValidator : AbstractValidator<CreateDeveloperCommand>
{
	public CreateDeveloperCommandValidator()
	{
        RuleFor(x => x.Developer)
            .SetValidator(new DeveloperValidator());
    }
}
