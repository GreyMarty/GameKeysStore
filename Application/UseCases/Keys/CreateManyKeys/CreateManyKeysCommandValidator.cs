using Application.Validation;
using FluentValidation;

namespace Application.UseCases.Keys.CreateManyKeys;

internal class CreateManyKeysCommandValidator : AbstractValidator<CreateManyKeysCommand>
{
    public CreateManyKeysCommandValidator()
    {
        RuleFor(x => x.Keys)
            .SetValidator(new KeysValidator());
    }
}
