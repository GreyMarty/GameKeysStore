using Application.Validation;

namespace Application.Results;

public struct ValidationFailed
{
    public IEnumerable<ValidationError> Errors { get; } = Enumerable.Empty<ValidationError>();

    public ValidationFailed()
    {
    }

    public ValidationFailed(string propertyName, string message) : this(new ValidationError(propertyName, message))
    {
    }

    public ValidationFailed(ValidationError error) : this(new[] { error })
    {
    }

    public ValidationFailed(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }
}