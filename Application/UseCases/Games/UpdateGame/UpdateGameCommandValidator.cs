using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Games.UpdateGame;

internal class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
	public UpdateGameCommandValidator()
	{
		RuleFor(x => x.Game)
			.SetValidator(new GameValidator());
	}
}
