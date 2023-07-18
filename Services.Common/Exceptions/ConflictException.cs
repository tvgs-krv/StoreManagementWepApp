namespace Services.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string description) : base(description) { }
}