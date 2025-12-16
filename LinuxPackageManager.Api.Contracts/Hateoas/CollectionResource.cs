namespace LinuxPackageManager.Api.Contracts.Hateoas;

public class CollectionResource<T> : ResourceBase
{
    
    public List<T> Items { get; }

    public CollectionResource(List<T> items)
    {
        Items = items;
    }
}