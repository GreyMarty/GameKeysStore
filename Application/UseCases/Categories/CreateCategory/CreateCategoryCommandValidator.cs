using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Categories.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
	public CreateCategoryCommandValidator()
	{
		RuleFor(x => x.Category)
			.SetValidator(new CategoryValidator());
	}
}
