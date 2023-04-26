using System.Text.RegularExpressions;

namespace Application.Validation;

internal static class Must
{
    public static bool BeValidName(string? name)
    {
        return Regex.IsMatch(name ?? String.Empty, RegexPatterns.ValidName);
    }

    public static bool BeValidSystemRequirementName(string? requirementName)
    {
        return Regex.IsMatch(requirementName ?? String.Empty, RegexPatterns.ValidSystemRequirementName);
    }
}