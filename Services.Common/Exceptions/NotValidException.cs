namespace Services.Common.Exceptions;

public class NotValidException : Exception
{
    public NotValidException(string description) : base(description) { }
}