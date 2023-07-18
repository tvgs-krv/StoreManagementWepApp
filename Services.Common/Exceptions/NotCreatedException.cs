namespace Services.Common.Exceptions;

public class NotCreatedException : Exception
{
    public NotCreatedException(string description) : base(description) { }
}