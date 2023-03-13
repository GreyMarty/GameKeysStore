namespace Application.Validation;

public static class ValidationMessages
{
    public static string MustNotBeEmpty => @"{PropertyName} must not be empty";

    public static string LengthMustBeInRange =>
        @"{PropertyName} must be between {MinLength} and {MaxLength} characters long";

    public static string MustBeValidName =>
        @"{PropertyName} allowed to have only following characters: 'a-zA-Z0-9_'";

    public static string MustBeValidSystemRequirementName =>
        @"{PropertyName} allowed to have only following characters: 'a-zA-Z0-9_-'";

    public static string MustNotBeNegative =>
        @"{PropertyName} must not be negative";
}