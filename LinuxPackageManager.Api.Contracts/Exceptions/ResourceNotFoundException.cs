namespace LinuxPackageManager.Api.Contracts.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string resource, object id)
        : base($"Resource with ID {id} not found")
    {
    }
}