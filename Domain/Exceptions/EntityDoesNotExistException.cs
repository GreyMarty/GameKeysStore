namespace Domain.Exceptions;

public class EntityDoesNotExistException : Exception
{
    public EntityDoesNotExistException()
    {
    }

    public EntityDoesNotExistException(string? message) : base(message)
    {
    }
}