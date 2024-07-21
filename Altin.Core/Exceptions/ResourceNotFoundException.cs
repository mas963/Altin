namespace Altin.Core;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() { }

    public ResourceNotFoundException(string message) : base(message) { }

    public ResourceNotFoundException(Type type) : base($"{type} is missing") { }
}
