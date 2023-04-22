using System.Runtime.Serialization;

namespace Application.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public string? PropertyPath;

    public EntityAlreadyExistsException()
    {
    }

    public EntityAlreadyExistsException(string? message) : base(message)
    {
    }

    public EntityAlreadyExistsException(string? message, string? propertyPath) : this(message) 
    {
        PropertyPath = propertyPath;
    }

    protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
