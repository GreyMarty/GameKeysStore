namespace Application.Results;

public struct AlreadyExists
{
	public string? Message { get; init; } = null;

	public AlreadyExists()
	{
	}

	public AlreadyExists(string? message)
	{
		Message = message;
	}
}
