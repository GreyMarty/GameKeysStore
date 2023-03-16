using Application.DTOs;
using FluentValidation;

namespace Application.Validation;

public class RegistrationUserValidator : AbstractValidator<RegistrationUserDto>
{
    public RegistrationUserValidator()
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