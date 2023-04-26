namespace UI.Validation;

public interface IValidationErrorsManager
{
    string GetInvalidClass(string propertyName);
    string GetValidationError(string propertyName);
    void ResetValidationError(string propertyName);
    void SetValidationError(string propertyName, string error);
}

public class ValidationErrorsManager : IValidationErrorsManager
{
    private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

    public void SetValidationError(string propertyName, string error) =>
        _validationErrors[propertyName] = error;

    public void ResetValidationError(string propertyName) =>
        _validationErrors.Remove(propertyName);

    public string GetValidationError(string propertyName) =>
        _validationErrors.ContainsKey(propertyName)
            ? _validationErrors[propertyName]
            : string.Empty;

    public string GetInvalidClass(string propertyName) =>
        _validationErrors.ContainsKey(propertyName) ? "is-invalid" : string.Empty;
}
