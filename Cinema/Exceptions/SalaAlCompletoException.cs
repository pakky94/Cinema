using System.Runtime.Serialization;

namespace CinemaClient.Exceptions;

public class SalaAlCompletoException : Exception
{
    public SalaAlCompletoException()
    {
    }

    public SalaAlCompletoException(string? message) : base(message)
    {
    }

    public SalaAlCompletoException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected SalaAlCompletoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
