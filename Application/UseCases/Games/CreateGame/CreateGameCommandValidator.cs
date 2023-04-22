using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Games.CreateGame;

internal class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
	public CreateGameCommandValidator()
	{
		RuleFor(x => x.Game)
			.SetValidator(new GameValidator());
	}
}
