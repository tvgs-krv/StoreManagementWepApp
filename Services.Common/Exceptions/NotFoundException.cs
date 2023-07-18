namespace Services.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Запись \"{name}\" ({key}) не удалось найти.") { }

    public NotFoundException(string description) : base(description) { }
}