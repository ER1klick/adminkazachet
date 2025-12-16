using LinuxPackageManager.Api.Contracts.Dto;
using LinuxPackageManager.Api.Contracts.EndPoints;
using LinuxPackageManager.Api.Contracts.Hateoas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace LinuxPackageManager.Application;

public class PackageResourceAssembler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PackageResourceAssembler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private IUrlHelper GetUrlHelper()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            throw new InvalidOperationException("HttpContext is not available to generate URLs.");
        }
        return httpContext.RequestServices.GetRequiredService<IUrlHelper>();
    }

    public PackageResponse ToResource(PackageResponse package)
    {
        var urlHelper = GetUrlHelper();
        package.Links.Add(new LinkDto(
            urlHelper.Action(nameof(IPackagesApi.GetPackageById), "Packages", new { id = package.Id }),
            "self",
            "GET"));
        return package;
    }

    public CollectionResource<PackageResponse> ToCollectionResource(PagedList<PackageResponse> pagedList, string routeName)
    {
        var urlHelper = GetUrlHelper();
        var resourceItems = pagedList.Items.Select(ToResource).ToList();
        var collectionResource = new CollectionResource<PackageResponse>(resourceItems);

        collectionResource.Links.Add(new LinkDto(
            urlHelper.RouteUrl(routeName, new { pageNumber = pagedList.PageNumber, pageSize = pagedList.PageSize }),
            "self", "GET"));

        if (pagedList.HasNextPage)
        {
            collectionResource.Links.Add(new LinkDto(
                urlHelper.RouteUrl(routeName, new { pageNumber = pagedList.PageNumber + 1, pageSize = pagedList.PageSize }),
                "next", "GET"));
        }

        if (pagedList.HasPreviousPage)
        {
            collectionResource.Links.Add(new LinkDto(
                urlHelper.RouteUrl(routeName, new { pageNumber = pagedList.PageNumber - 1, pageSize = pagedList.PageSize }),
                "prev", "GET"));
        }

        return collectionResource;
    }
}