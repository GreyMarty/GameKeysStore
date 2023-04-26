using Application.Models.WriteModels;
using FluentValidation;

namespace Application.Validation;

public class UserValidator : AbstractValidator<UserWriteModel>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .Length(3, 128)
            .WithMessage(ValidationMessages.LengthMustBeInRange)
            .Must(Must.BeValidName)
            .WithMessage(ValidationMessages.MustBeValidName);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationMessages.MustBeValidEmail);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 256)
            .WithMessage(ValidationMessages.LengthMustBeInRange);
    }
}