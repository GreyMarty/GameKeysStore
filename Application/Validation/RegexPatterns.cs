namespace Application.Validation;

public static class RegexPatterns
{
    public static string ValidName => @"^[ a-zA-Z0-9_]*$";

    public static string ValidSystemRequirementName => @"^[ a-zA-Z0-9_\-/,]*$";
}