namespace Services.Common.Exceptions;

public class NotSendException : Exception
{
    public NotSendException(string description) : base(description) { }
}