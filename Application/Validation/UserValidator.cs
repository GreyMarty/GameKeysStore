using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class UserValidator : AbstractValidator<UserWriteModel>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName)
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(ValidationMessages.MustBeValidEmail);

        // TODO
        // Add password validation
    }
}