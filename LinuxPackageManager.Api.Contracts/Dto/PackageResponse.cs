namespace LinuxPackageManager.Api.Contracts.Dto;

using LinuxPackageManager.Api.Contracts.Hateoas;

public class PackageResponse : ResourceBase
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Version { get; init; }
    public string Architecture { get; init; }
    public string RepositoryName { get; init; }
    public DateTime CreatedAt { get; init; }
    
    public PackageResponse(long id, string name, string version, string architecture, string repositoryName, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Version = version;
        Architecture = architecture;
        RepositoryName = repositoryName;
        CreatedAt = createdAt;
    }
}