namespace LinuxPackageManager.Api.Contracts.Hateoas;

using System.Text.Json.Serialization;

public abstract class ResourceBase
{
    public List<LinkDto> Links { get; set; } = new();
}