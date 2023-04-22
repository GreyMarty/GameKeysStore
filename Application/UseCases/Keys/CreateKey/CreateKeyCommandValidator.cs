using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Keys.CreateKey;

internal class CreateKeyCommandValidator : AbstractValidator<CreateKeyCommand>
{
	public CreateKeyCommandValidator()
	{
		RuleFor(x => x.Key)
			.SetValidator(new KeyValidator());
	}
}
