using System.Text.RegularExpressions;

namespace Application.Validation;

internal static class Must
{
    public static bool BeValidName(string name)
    {
        return Regex.IsMatch(name, RegexPatterns.ValidName);
    }

    public static bool BeValidSystemRequirementName(string requirementName)
    {
        return Regex.IsMatch(requirementName, RegexPatterns.ValidSystemRequirementName);
    }
}