using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

internal class CategoryValidator : AbstractValidator<CategoryWriteModel>
{
	public CategoryValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Must(Must.BeValidName)
			.WithMessage(ValidationMessages.MustBeValidName);
	}
}
