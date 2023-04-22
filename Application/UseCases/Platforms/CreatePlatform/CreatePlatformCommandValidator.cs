using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Platforms.CreatePlatform;

public class CreatePlatformCommandValidator : AbstractValidator<CreatePlatformCommand>
{
	public CreatePlatformCommandValidator()
	{
		RuleFor(x => x.Platform)
			.SetValidator(new PlatformValidator());
	}
}
